#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Process
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
	/// <see cref="Process" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessData" />.
	/// </summary>
	public abstract class Process : WorkflowObject
	{
		private ProcessData mProcessData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Process"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="processData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessData" /></param>
		protected Process(Client client, ProcessData processData): base(client, processData)
		{
			if (processData == null)
				throw new ArgumentNullException("processData");

			mProcessData = processData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Process"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Process(Client client, TcmUri uri): this(client, client.Read<ProcessData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Process" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessData" />
		/// </summary>
		/// <param name="processData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessData" /></param>
		protected void Reload(ProcessData processData)
		{
			if (processData == null)
				throw new ArgumentNullException("processData");

			mProcessData = processData;
			base.Reload(processData);
		}

		/// <summary>
		/// Reload the <see cref="Process" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ProcessData>(this.Id));
		}
	}
}
