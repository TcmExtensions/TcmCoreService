#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Process History
// ---------------------------------------------------------------------------------
//	Date Created	: March 22, 2014
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
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ProcessHistory" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessHistoryData" />.
	/// </summary>
	public class ProcessHistory : Process
	{
		private ProcessHistoryData mProcessHistoryData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessHistory"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="processHistoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessHistoryData" /></param>
		protected ProcessHistory(Session session, ProcessHistoryData processHistoryData): base(session, processHistoryData)
		{
			if (processHistoryData == null)
				throw new ArgumentNullException("processHistoryData");

			mProcessHistoryData = processHistoryData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessHistory"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ProcessHistory(Session session, TcmUri uri): this(session, session.Read<ProcessHistoryData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ProcessHistory" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessHistoryData" />
		/// </summary>
		/// <param name="processHistoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessHistoryData" /></param>
		protected void Reload(ProcessHistoryData processHistoryData)
		{
			if (processHistoryData == null)
				throw new ArgumentNullException("processHistoryData");

			mProcessHistoryData = processHistoryData;
			base.Reload(processHistoryData);
		}

		/// <summary>
		/// Reload the <see cref="ProcessHistory" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<ProcessHistoryData>(this.Id));
		}
	}
}
