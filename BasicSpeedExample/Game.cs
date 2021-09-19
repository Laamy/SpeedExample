#region

using System;

#endregion

namespace BasicSpeedExample
{
    class GameOffsets
    {
        public static ulong velocityOffset = 0x50C;
        public static ulong bodyRotationOffset = 0x148;
    }
    class Game
    {
        public static ulong clientInstance // Game > ClientInstance
        { get => MCM.baseEvaluatePointer(0x041457D8, new ulong[] { 0x0, 0x20 }); } // clientInstance
        public static ulong localPlayer // Game > ClientInstance > LocalPlayer
        { get => MCM.evaluatePointer(clientInstance, new ulong[] { 0xC8, 0x0 }); } // localPlayer
                                                                                   // all this means is Minecraft.Windows.exe+041457D8,0,20,C8,0(LocalPlayerPointer 1.17)
                                                                                   // we put 0x before a hex number to tell the programing language that its hex
                                                                                   // to fix this all you have to do is replace the broken pointer information

        public static Vector3 velocity // Game > ClientInstance > LocalPlayer > Velocity
        {
            get
            {
                var vec = new Vector3(0, 0, 0);

                vec.x = MCM.readFloat(localPlayer + GameOffsets.velocityOffset); // X Velocity
                vec.y = MCM.readFloat(localPlayer + GameOffsets.velocityOffset + 4); // Y Velocity
                vec.z = MCM.readFloat(localPlayer + GameOffsets.velocityOffset + 8); // Z Velocity

                return vec;
            }
            set
            {
                MCM.writeFloat(localPlayer + GameOffsets.velocityOffset, value.x); // X Velocity
                MCM.writeFloat(localPlayer + GameOffsets.velocityOffset + 4, value.y); // Y Velocity
                MCM.writeFloat(localPlayer + GameOffsets.velocityOffset + 8, value.z); // Z Velocity
            }
        } // Velocity

        public static Vector3 bodyRots // Game > ClientInstance > LocalPlayer > BodyRotations
        {
            get
            {
                var vec = new Vector3(0, 0, 0);

                vec.x = MCM.readFloat(localPlayer + GameOffsets.bodyRotationOffset); // Yaw
                vec.y = MCM.readFloat(localPlayer + GameOffsets.bodyRotationOffset + 4); // Pitch

                return vec;
            }
            // set { }
            // cant set this due to some set factors ill explain later but setting it will
            // come in handy for external killaura without lookingAtEntityAddress being used
        } // Velocity

        public static Vector3 lVector
        {
            get
            {
                Vector3 tempVec;

                var cYaw = (bodyRots.y + 89.9f) * (float)Math.PI / 178f;
                var cPitch = bodyRots.x * (float)Math.PI / 178f;

                tempVec = dirVect(cYaw, cPitch);

                return tempVec;
            }
            // set { }
        } // Looking Vector

        public static Vector3 dirVect(float x, float y) // pretty sure this is fine wtf
        {
            var tempVec = new Vector3(0, 0, 0); // create empty vector

            tempVec.x = (float)Math.Cos(x) * (float)Math.Cos(y);
            tempVec.y = (float)Math.Sin(y);
            tempVec.z = (float)Math.Sin(x) * (float)Math.Cos(y);

            return tempVec;
        } // Directional Vector
    }
}
