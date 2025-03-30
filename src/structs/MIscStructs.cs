using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mdl_parser.src.structs { 
    public class Vec3 {
        public float X;
        public float Y;
        public float Z;

        // CrossProduct method
        public Vec3 CrossProduct(Vec3 otherVector) {
            Vec3 crossVector = new Vec3();

            crossVector.X = this.Y * otherVector.Z - this.Z * otherVector.Y;
            crossVector.Y = this.Z * otherVector.X - this.X * otherVector.Z;
            crossVector.Z = this.X * otherVector.Y - this.Y * otherVector.X;

            return crossVector;
        }

        // Normalize method
        public Vec3 Normalize() {
            double magnitude = Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            Vec3 normalVector = new Vec3();

            normalVector.X = (float)(this.X / magnitude);
            normalVector.Y = (float)(this.Y / magnitude);
            normalVector.Z = (float)(this.Z / magnitude);

            return normalVector;
        }
    }

    public class Vec4 {
        public double X;
        public double Y;
        public double Z;
        public double W;
    }
}
