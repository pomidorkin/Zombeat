using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleClass
{
    // All possible key & bpm combinations should be defined here as a class.
    public enum VehicleClassType
    {
        UNDEFINED,
        NEONVORTEX, // A# min, 130 BPM
        HEAVYDUTY

    }

    public static VehicleClassType CheckClass(SoundUnitKey key, int bpm)
    {
        switch ((key, bpm))
        {
            case (SoundUnitKey.A_SHARP_M, 130):
                return VehicleClassType.NEONVORTEX;
            case (SoundUnitKey.D_SHARP_M, 130):
                return VehicleClassType.HEAVYDUTY;
            default:
                return VehicleClassType.UNDEFINED;
        }
    }
}
