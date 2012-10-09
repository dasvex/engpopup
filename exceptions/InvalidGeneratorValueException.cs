namespace EngPopup.exceptions {
    [System.Serializable]
    public class InvalidGeneratorValueException : System.Exception {
        public InvalidGeneratorValueException() {
        }
        public InvalidGeneratorValueException(string message) : base(message) {
        }
        public InvalidGeneratorValueException(string message,System.Exception inner) : base(message,inner) {
        }
        protected InvalidGeneratorValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info,context) {
        }
    }
    [System.Serializable]
    public class NegativeBorderException : InvalidGeneratorValueException {
        public NegativeBorderException()
            :base("Border of triangle distribution can not be negative value."){
        }
        public NegativeBorderException(string message) : base(message) {
        }
        public NegativeBorderException(string message,System.Exception inner) : base(message,inner) {
        }
        protected NegativeBorderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info,context) {
        }
    }
    [System.Serializable]
    public class MedianOutOfRangeException : InvalidGeneratorValueException {
        public MedianOutOfRangeException()
            :base(@"Median line can not be more than right border and less that left border."){
        }
        public MedianOutOfRangeException(string message) : base(message) {
        }
        public MedianOutOfRangeException(string message,System.Exception inner) : base(message,inner) {
        }
        protected MedianOutOfRangeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info,context) {
        }
    }
    [System.Serializable]
    public class BorderSwitchException : InvalidGeneratorValueException
    {
      public BorderSwitchException() 
        :base(@"Try switch border.Can not set right border less than left border and left border can not be more than right border."){ 
      }
      public BorderSwitchException( string message ) : base( message ) { }
      public BorderSwitchException( string message, System.Exception inner ) : base( message, inner ) { }
      protected BorderSwitchException( 
	    System.Runtime.Serialization.SerializationInfo info, 
	    System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
    }

}
