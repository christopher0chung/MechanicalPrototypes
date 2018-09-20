using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SCG_InputBuffer : MonoBehaviour {

    //InputBuffer is a manager style component used as a lookup for various key states
    //A public list of keys can be created for the input buffer to monitor
    //InputBuffer tracks and exposes the following conditions
    //-- KeyDown
    //-- IsHeld
    //-- BufferedKeyHold
    //-- KeyUp
    //-- KeyRate
    //-- TimeSinceInactive

    #region Public Properties
    public List<KeyCode> trackedKeys = new List<KeyCode>();
    #endregion

    #region Private Properties
    private float holdBufferThreshold = 0.15f;

    private List<BufferedInputTracker> _activeKeys = new List<BufferedInputTracker>();

    private Dictionary<KeyCode, BufferedInputTracker> keyToBuffer = new Dictionary<KeyCode, BufferedInputTracker>();
    #endregion

    void Start () {
        _TrackedKeysCleanupRepeats();
        _InitializeBuffers();
        _CreateDictionary();
	}
	
	void Update () {
        _UpdateBuffers();
    }

    #region Private Functions
    private void _TrackedKeysCleanupRepeats()
    {
        for (int i = trackedKeys.Count - 1; i >= 0; i--)
        {
            for (int j = i -1; j >= 0; j--)
            {
                if (trackedKeys[i] == trackedKeys[j])
                {
                    trackedKeys.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void _InitializeBuffers()
    {
        for (int i = 0; i < trackedKeys.Count; i++)
        {
            _activeKeys.Add(new BufferedInputTracker(trackedKeys[i], holdBufferThreshold));
        }
    }

    private void _CreateDictionary()
    {
        KeyCode k;

        for (int i = 0; i < trackedKeys.Count; i++)
        {
            k = trackedKeys[i];
            for (int j = 0; j < _activeKeys.Count; j++)
            {
                if (_activeKeys[j]._keyCode == k)
                {
                    keyToBuffer.Add(k, _activeKeys[j]);
                }
            }
        }
    }

    private void _UpdateBuffers()
    {
        for (int i = 0; i < _activeKeys.Count; i++)
        {
            _activeKeys[i].ScheduledUpdate();
        }
    }

    private BufferedInputTracker _BufferCheck(Type c, KeyCode kc)
    {
        Debug.Assert(trackedKeys.Contains(kc), c + " is requesting a non-tracked key");

        BufferedInputTracker b;
        keyToBuffer.TryGetValue(kc, out b);

        Debug.Assert(b != null, "Dictionary cannot find associated buffer");

        return b;
    }
    #endregion

    #region Public Functions
    public bool KeyDown (Type caller, KeyCode keyCode)
    {
        BufferedInputTracker b = _BufferCheck(caller, keyCode);

        return b.keyDown;
    }

    public bool IsHeld(Type caller, KeyCode keyCode)
    {
        BufferedInputTracker b = _BufferCheck(caller, keyCode);

        return b.isHeld;
    }

    public bool KeyUp(Type caller, KeyCode keyCode)
    {
        BufferedInputTracker b = _BufferCheck(caller, keyCode);

        return b.keyUp;
    }

    public float KeyRate(Type caller, KeyCode keyCode)
    {
        BufferedInputTracker b = _BufferCheck(caller, keyCode);

        return b.keyRate;
    }

    #endregion

    #region Internal Classes
    public class BufferedInputTracker
    {
        public KeyCode _keyCode;

        public bool keyDown;
        public bool isHeld;
        public bool keyUp;
        public bool bufferedKeyHold;
        public float keyRate;
        public float timeSinceInactive;

        private List<TapTime> _tapTimes;

        private bool _bA;
        private bool _bufferActive
        {
            get
            {
                return _bA;
            }
            set
            {
                if (value != _bA)
                {
                    _bA = value;
                    if (!_bA)
                    {
                        _tapTimes.Clear();
                    }
                }
            }
        }
        private float _bufferClock;
        private float _internalBufferThreshold;
 
        public BufferedInputTracker(KeyCode keyCode, float bufferThreshold)
        {
            _keyCode = keyCode;
            _internalBufferThreshold = bufferThreshold;
            _tapTimes = new List<TapTime>();
        }

        public void ScheduledUpdate()
        {
            _SetBufferFlags();
            _UpdateBufferClock();
            _CalculateKeyRate();
        }

        private void _SetBufferFlags()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                keyDown = true;
                _tapTimes.Add(new TapTime());
                _tapTimes[_tapTimes.Count - 1].pressTime = Time.time;
            }
            else
                keyDown = false;

            if (Input.GetKey(_keyCode))
            {
                isHeld = true;
                _tapTimes[_tapTimes.Count - 1].pressTime += Time.deltaTime;
                _ResetBufferClock();
            }
            else
                isHeld = false;

            if (Input.GetKeyUp(_keyCode))
                keyUp = true;

            else
                keyUp = false;
        }

        private void _UpdateBufferClock()
        {
            _bufferClock += Time.deltaTime;
            if (_bufferClock <= _internalBufferThreshold)
                _bufferActive = true;
            else
                _bufferActive = false;
        }

        private void _ResetBufferClock()
        {
            _bufferClock = 0;
        }

        private void _CalculateKeyRate()
        {
            if (_tapTimes.Count <= 1)
            {
                keyRate = 0;
            }
            else
            {
                float sum = 0;
                float thisTime = 0;
                float nextTime = 0;

                for (int i = 0; i < _tapTimes.Count - 1; i++)
                {
                    nextTime = _tapTimes[i + 1].pressTime;
                    thisTime = _tapTimes[i].pressTime;

                    sum += nextTime - thisTime;
                }

                keyRate = (_tapTimes.Count - 1) / sum;
            }
        }
    }

    public class TapTime
    {
        public float pressTime;
        public float holdTime;
    }
    #endregion
}