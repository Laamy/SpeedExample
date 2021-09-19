#region

using System;

#endregion

namespace BasicSpeedExample
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(string position)
        {
            try
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToSingle(parsedStr[0]);
                y = Convert.ToSingle(parsedStr[1]);
                z = Convert.ToSingle(parsedStr[2]);
            }
            catch
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToInt64(parsedStr[0], 16);
                y = Convert.ToInt64(parsedStr[1], 16);
                z = Convert.ToInt64(parsedStr[2], 16);
            }
        }

        public float DistanceTo(Vector3 _Vec3)
        {
            float diff_x = x - _Vec3.x, diff_y = y - _Vec3.y, diff_z = z - _Vec3.z;
            var output = (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
            if ((int)output == 0) output = _Vec3.Distance(this);
            return output;
        }

        public float Distance(Vector3 _Vec3)
        {
            float diff_x = x - _Vec3.x, diff_y = y - _Vec3.y, diff_z = z - _Vec3.z;
            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
        } // messy distance i just neve cleaned up plz ignore ;-;

        public override string ToString()
        {
            return x + "," + y + "," + z;
        }

        internal float Distance(object postion)
        {
            throw new NotImplementedException();
        }
    }

    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(string position)
        {
            try
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToSingle(parsedStr[0]);
                y = Convert.ToSingle(parsedStr[1]);
            }
            catch
            {
                var parsedStr = position.Replace(" ", "").Split(',');
                x = Convert.ToInt64(parsedStr[0], 16);
                y = Convert.ToInt64(parsedStr[1], 16);
            }
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    }

    public struct AABB
    {
        public Vector3 lower;
        public Vector3 upper;

        public AABB(Vector3 lower, Vector3 upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }
}
