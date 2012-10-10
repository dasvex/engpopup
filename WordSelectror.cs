using System;
using EngPopup.exceptions;
using Troschuetz.Random;
using SqliteGateWay;

namespace WordSelector {
    ///незя геренить тольок отрицательные будет ппц в некст вал
    /// <summary>
    /// аальфа - нижний предел
    /// бетта верхний предел
    /// гамма -центровая
    /// альфа должна быть на 20 % ЛЕВЕЕ установленной
    /// бетта должна быть на 20 % ПРАВЕЕ установленной
    /// </summary>
    class WordSelector {
        private Generator generator = new Generator(770,800,780);
        public int Betta {
            get{
                return generator.RightBorder;
            }
            set{
                generator.SetBordersAndMedian(generator.LeftBorder,value,generator.Median);
            }
        }
        public int Alpha {
            get {
                return generator.LeftBorder;
            }
            set {
                generator.SetBordersAndMedian(value,generator.RightBorder,generator.Median);
            }
        }
        public int Gamma{
            get{
                return generator.Median;
            }
            set{
                generator.SetBordersAndMedian(generator.LeftBorder,generator.RightBorder,value);
            }
        }
        private class Generator {
            private const double DISTRIBUTION_CORRECTION = 0.2; // подинмает характеристику на 20 процентов
            private int RealAlpha {
                get;
                set;
            }
            private int RealBeta {
                get;
                set;
            }
            private Troschuetz.Random.TriangularDistribution  distribution= new TriangularDistribution(new XorShift128Generator());
            private bool IsMedianNotBeetwenBorders(int leftBorder,int rightBorder,int median) {
                return leftBorder > median || rightBorder < median ? true : false;
            }
            private bool IsNotPositiveBorder(int border) {
                return border < 0 ? true : false;
            }
            private bool IsInvalidPositionBorders(int leftBorder,int rightBorder) {
                return rightBorder <= leftBorder ? true : false;
            }
            private double OffsetBorders() {
                return (this.RightBorder - this.LeftBorder) * DISTRIBUTION_CORRECTION;
            }
            private void CorrectionBorders() {
                int _alpha = Convert.ToInt32(this.LeftBorder - this.OffsetBorders());
                int _beta  = Convert.ToInt32(this.RightBorder+ this.OffsetBorders());
                this.LeftBorder = _alpha;
                this.RightBorder = _beta;
            }


            public int LeftBorder {
                get {
                    return (int)distribution.Alpha;
                }
                private set {
                    distribution.Alpha = value;
                }
            }
            public int RightBorder {
                get{
                    return (int)distribution.Beta;
                }
                private set{
                    distribution.Beta = value;
                }
            }
            public int Median{
                get{
                    return (int)distribution.Gamma;
                }
                private set{
                    distribution.Gamma = value;
                }
            }

            public int GenerateNextSignedValue() {
                int RandomVal=-1;
                while(RandomVal < RealAlpha || RandomVal>RealBeta)
                    RandomVal = Convert.ToInt32(distribution.NextDouble());
                return RandomVal;
            }
            public bool Reset(){
                return distribution.Reset();
            }
            public void SetBordersAndMedian(int leftBorder, int rightBorder, int median) {
                if(this.IsInvalidPositionBorders(leftBorder,rightBorder))
                    throw new BorderSwitchException();
                if(this.IsMedianNotBeetwenBorders(leftBorder,rightBorder,median))
                    throw new MedianOutOfRangeException();
                if(this.IsNotPositiveBorder(leftBorder))
                    throw new NegativeBorderException();
                if(this.IsNotPositiveBorder(rightBorder))
                    throw new NegativeBorderException();

                if(leftBorder > this.LeftBorder && rightBorder < this.RightBorder)
                     this.Median = median;
                if(leftBorder <= this.LeftBorder)
                     this.LeftBorder = leftBorder;
                if(rightBorder >= this.RightBorder)
                     this.RightBorder = rightBorder;
                this.LeftBorder = leftBorder;
                this.RightBorder = rightBorder;
                this.Median = median;

                if(leftBorder <= this.LeftBorder) {
                    this.LeftBorder = leftBorder;
                    this.Median = median;
                    this.RightBorder = rightBorder;
                } else {
                    this.RightBorder = rightBorder;
                    this.Median = median;
                    this.LeftBorder = leftBorder;
                }
                this.RealAlpha = leftBorder;
                this.RealBeta = rightBorder;
                this.CorrectionBorders();
            }
            public Generator(int alpha,int beta,int gamma) {
                SetBordersAndMedian(alpha,beta,gamma);
            }
        }
        public int NextValue() {
            return generator.GenerateNextSignedValue();
        }
        public WordSelector() {
        }
        public bool Reset(){
            return  generator.Reset();
        }
        public Standart_2500Record GetWord() {
            Standart_2500Record row = new Standart_2500Record();
            System.Diagnostics.Debug.WriteLine(this.NextValue());
            row.freq = this.NextValue();
            if (row.SelectByFreq() )
                return row;
            row.word = @"error get word by freq";
            row.trans = "";
            row.id = 0;
            return row;
        }  //переписать говно это 
    }
}


 