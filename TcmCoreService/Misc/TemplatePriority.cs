#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Template Priority
// ---------------------------------------------------------------------------------
//	Date Created	: March 24, 2014
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

namespace TcmCoreService.Misc
{
	/// <summary>
	/// <see cref="TemplatePriority" /> maps <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> publishing priorities
	/// </summary>
	public enum TemplatePriority
	{
		/// <summary>
		/// Never link to this template
		/// </summary>
		NeverLink = 0,
		/// <summary>
		/// Low linking priority
		/// </summary>
		Low = 100,
		/// <summary>
		/// Medium linking priority
		/// </summary>
		Medium = 200,
		/// <summary>
		/// High linking priority
		/// </summary>
		High = 300
	}
}
