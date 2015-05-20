using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SQReder.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VectorEx
    {
        public PointF Coordinates = new PointF(0, 0);
        public Vector Vector = new Vector(0, 0);

        #region constructors
        public VectorEx(PointF coordinates, Vector vector)
        {
            Coordinates = coordinates;
            Vector = vector;
        }

        public VectorEx()
            : this(new PointF(), new Vector())
        { }

        public VectorEx(float x1, float y1, float x2, float y2)
            : this(new PointF(x1,y1), new PointF(x2,y2))
        {}

        public VectorEx(PointF begin, PointF end)
            : this(begin, new Vector(begin, end))
        { }

        public VectorEx(VectorEx vector)
            : this(vector.Coordinates, vector.Vector)
        { } 
        #endregion

        #region Интерфейс для быстрого доступа к подполям
        [JsonProperty]
        public float Angle
        {
            get { return Vector.a; }
            set { Vector.a = value; }
        }

        [JsonProperty]
        public float Radius
        {
            get { return Vector.r; }
            set { Vector.r = value; }
        }

        [JsonProperty]
        public float X
        {
            get { return Coordinates.X; }
            set { Coordinates.X = value; }
        }

        [JsonProperty]
        public float Y
        {
            get { return Coordinates.Y; }
            set { Coordinates.Y = value; }
        }

        public PointF Begin
        {
            get { return Coordinates; }
        }

        public PointF End
        {
            get { return Begin + Vector.ToSizeF(); }
        }

        #endregion  Интерфейс для быстрого доступа к подполям

        /// <summary>
        /// откладывает один вектор от другого
        /// </summary>
        /// <param name="v">вектор от которого откладывается текущий</param>
        /// <returns>новый вектор,</returns>
        public KeyValuePair<PointF, PointF> PutOff(VectorEx v)
        {
            var d = v + this;
            var a = d.Coordinates;
            var b = d.Vector.Point;
            b += new SizeF(d.Coordinates.X, d.Coordinates.Y);
            return new KeyValuePair<PointF, PointF>(a, b);
        }

        /// <summary>
        /// Откладывает один вектор от другого с учетом поворота
        /// </summary>
        /// <param name="baseVector"></param>
        /// <param name="morfingVector"></param>
        /// <returns></returns>
        public static VectorEx operator +(VectorEx baseVector, VectorEx morfingVector)
        {
            //d := QuadToPolar(b.xy); // координату начала откладываемого в вектор
            var d = new Vector(morfingVector.Coordinates);
            //d.a := d.a + a.ra.a; // поворачиваем начальную коордитнату откладываемого на угол образующего
            // поворачиваем вектор на угол базового
            d.a += baseVector.Vector.a;

            //result.xy := PolarToQuad(d); // и преобразовываем в точку.
            //result.xy := AddCoord(result.xy, c); // откладываем от конца образующего вектора
            var newCoords = d.Point + new SizeF(baseVector.End.X, baseVector.End.Y);
            //result.ra := b.ra; // а вектор на выходе просто вращаем на угол образующего
            var newVector = morfingVector.Vector;
            //result.ra.a := result.ra.a + a.ra.a;
            newVector.a += baseVector.Vector.a;

            return new VectorEx(newCoords, newVector);
        }

        /// <summary>
        /// Умноженик на скаляр (координаты начата тоже умножаются)
        /// </summary>
        /// <param name="vector">масштабиреумый вектор</param>
        /// <param name="k">коэффицент масштабирования</param>
        /// <returns>масштабированный вектор</returns>
        public static VectorEx operator *(VectorEx vector, float k)
        {
            return new VectorEx { X = vector.X * k, Y = vector.Y * k, Radius = vector.Radius * k, Angle = vector.Angle };
        }

        public VectorEx Inverted
        {
            get { return new VectorEx(End, Begin); }
        }

        public override string ToString()
        {
            return Coordinates + " " + Vector;
        }
    }
}
