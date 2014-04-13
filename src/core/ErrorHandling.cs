﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiPascal.core
{
	public class CrossPascalException : Exception
	{
		const string DefaultMsg = "Compilation Error";
	
		public CrossPascalException (int lineno, string message = DefaultMsg)
			: base (message + " in line " + lineno) { }

		public CrossPascalException(string message = DefaultMsg)
			: base (message) { }
	}


	//
	// Internal Errors
	//

	public class AstNodeException : CrossPascalException
	{
		const string DefaultMsg = "Error creating an AST Node";

		public AstNodeException (int lineno, string message = DefaultMsg) : base (lineno,message) { }
		
		public AstNodeException (string message = DefaultMsg) : base (message) { }
	}

	public class NotImplementedException : CrossPascalException
	{
		const string DefaultMsg = " not implemented";

		public NotImplementedException(int lineno, string message) : base(lineno, message + DefaultMsg) { }

		public NotImplementedException(string message) : base(message + DefaultMsg) { }
	}

	public class InvalidAbstractException : CrossPascalException
	{
		const string DefaultMsg = " method should never be called";

		public InvalidAbstractException(int lineno, string message) : base(lineno, message + DefaultMsg) { }

		public InvalidAbstractException(string message) : base(message + DefaultMsg) { }

		public InvalidAbstractException(int lineno, Object thrower, string met) : this(lineno, thrower + " " + met) { }

		public InvalidAbstractException(Object thrower, string met) : this(thrower + " " + met) { }
	}


}
