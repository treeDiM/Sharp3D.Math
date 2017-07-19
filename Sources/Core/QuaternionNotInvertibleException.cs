#region Sharp3D.Math, Copyright(C) 2004 Eran Kampf, Licensed under LGPL.
//	Sharp3D.Math math library
//	Copyright (C) 2003-2004  
//	Eran Kampf
//	eran@ekampf.com
//	http://www.ekampf.com
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion
#region Using directives

using System;
using System.Runtime.Serialization;

#endregion

namespace Sharp3D.Math
{
	/// <summary>
	/// Base class for all exceptions thrown by Sharp3D.Math.
	/// </summary>
	[Serializable]
	public class QuaternionNotInvertibleException : MathException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionNotInvertibleException"/> class.
		/// </summary>
		public QuaternionNotInvertibleException() : base() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionNotInvertibleException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public QuaternionNotInvertibleException(string message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionNotInvertibleException"/> class 
		/// with a specified error message and a reference to the inner exception that is 
		/// the cause of this exception.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">
		/// The exception that is the cause of the current exception. 
		/// If the innerException parameter is not a null reference, the current exception is raised 
		/// in a catch block that handles the inner exception.
		/// </param>
		public QuaternionNotInvertibleException(string message, Exception inner) : base(message, inner) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="QuaternionNotInvertibleException"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected QuaternionNotInvertibleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
