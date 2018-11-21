using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MP3
{
    namespace Application
    {
        public class GeneralTypes {

            public enum ID { _0, _1 }
            public enum Items { ArTank, O2Tank, Battery, PatchPlate}
            public enum Equipment { ArCharger, O2Charger, BatteryCharger, Welder, PneumaticOperator, Locker }
            public enum Station { Helm, LeeHelm, Winch }
            public enum MenuVerbs { Operate, Install, Remove, Read }
            public enum ControlStates { Character, Menu, Station, InterruptableAction }
            public enum FacingDirection { Right, Left }
        }
    }
}
