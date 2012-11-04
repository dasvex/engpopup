using System;
using EngPopup.exceptions;
using Troschuetz.Random;
using SqliteGateWay;
using Generator;

namespace WordSelector {
    ///незя геренить тольок отрицательные будет ппц в некст вал
    /// <summary>
    /// аальфа - нижний предел
    /// бетта верхний предел
    /// гамма -центровая
    /// альфа должна быть на 20 % ЛЕВЕЕ установленной
    /// бетта должна быть на 20 % ПРАВЕЕ установленной
    /// </summary>
    

    ///генератор готов
    ///доделать обертку его класс вордселектор
    ///запретить менять альфа бетта ???
    ///подцтягивать бордеры из анализа таблиц 
    ///шлюз перессмотеть
    ///шлюз анализ таблиц
    ///
    ///
    public class WordSelector {
        private TriangleGenerator generator = new TriangleGenerator(700,40000,5000);
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
        
        public int NextValue() {
            return generator.GenerateNextSignedValue();
        }
        public WordSelector() {
        }
        public bool Reset(){
            return  generator.Reset();
        }
        public void SetBordersAndMedian(int leftBorder , int rightBorder , int median) {
            generator.SetBordersAndMedian(leftBorder,rightBorder,median);
        }
        public Record GetWord() {
            Record row = new Record(DictionList.standart);
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


 