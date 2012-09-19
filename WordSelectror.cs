using System;
using Troschuetz.Random;
using SqliteGateWay;

namespace WordSelector {
    class WordSelector {
        private Generator generator = new Generator();
        public double Mu {
            get{
                return generator.Mu;
            }
            set{
                generator.Mu=value;
            }
        }
        public double Sigma{
            get {
                return generator.Sigma;
            }
            set{
                generator.Sigma=value;
            }
        }
        private class Generator {
            private Troschuetz.Random.NormalDistribution  distribution= new NormalDistribution(new XorShift128Generator());
            public double Mu {
                get{
                    return distribution.Mu;
                }
                set{
                    distribution.Mu=value;
                }
            }
            public double Sigma{
                get {
                    return distribution.Sigma;
                }
                set{
                    distribution.Sigma=value;
                }
            }

            public Generator() {
                this.Mu = 1000;
                this.Sigma = 500;
            }
            public double GenerateDouble() {
                return   distribution.NextDouble();
            }
            public bool Reset(){
                return distribution.Reset();
            }

        }

        public bool Reset(){
            return  generator.Reset();
        }
        private int NextValue() {
            return Convert.ToInt32(1000 * generator.GenerateDouble());
        }
        public Standart_2500Record GetWord() {
            Standart_2500Record row = new Standart_2500Record();
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
