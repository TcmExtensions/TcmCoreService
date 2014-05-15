#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Info
// ---------------------------------------------------------------------------------
//	Date Created	: March 19, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="Info" /> is a base class for all info sub-classes
	/// </summary>
	public abstract class InfoBase
	{
		private Session mSession;

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoBase"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		protected InfoBase(Session session)
		{
			mSession = session;
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Session" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Session" />
		/// </value>
		protected Session Session
		{
			get
			{
				return mSession;
			}
		}
	}
}
