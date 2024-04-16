using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Exceptions
{
    [Serializable]
    public class DataConflictException : Exception
    {
        /// <summary>
        /// Conflict entities
        /// </summary>
        public IEnumerable<object> Entities { get; set; } = null!;

        /// <summary>
        /// Construct DataConflictException with conflict entities
        /// </summary>
        /// <param name="entities"></param>
        public DataConflictException(IEnumerable<object> entities)
        {
            Entities = entities;
        }

        /// <summary>
        /// Initializes a new instance of the InvalidCreateBudgetException class.
        /// </summary>
        public DataConflictException() { }

        /// <summary>
        /// Initializes a new instance of the InvalidCreateBudgetException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DataConflictException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the InvalidCreateBudgetException class with a specified error
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference
        ///    (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DataConflictException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the InvalidCreateBudgetException class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DataConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
