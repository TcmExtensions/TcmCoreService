#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Process Definition
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
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ProcessDefinition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionData" />.
	/// </summary>
	public abstract class ProcessDefinition : RepositoryLocalObject
	{
		private ProcessDefinitionData mProcessDefinitionData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessDefinition"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="processDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionData" /></param>
		protected ProcessDefinition(Session session, ProcessDefinitionData processDefinitionData): base(session, processDefinitionData)
		{
			if (processDefinitionData == null)
				throw new ArgumentNullException("processDefinitionData");

			mProcessDefinitionData = processDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessDefinition"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ProcessDefinition(Session session, TcmUri uri): this(session, session.Read<ProcessDefinitionData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ProcessDefinition" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionData" />
		/// </summary>
		/// <param name="processDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionData" /></param>
		protected void Reload(ProcessDefinitionData processDefinitionData)
		{
			if (processDefinitionData == null)
				throw new ArgumentNullException("processDefinitionData");

			mProcessDefinitionData = processDefinitionData;
			base.Reload(processDefinitionData);
		}

		/// <summary>
		/// Reload the <see cref="ProcessDefinition" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<ProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="ProcessDefinition" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<ProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="ProcessDefinition" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<ProcessDefinitionData>(this.Id));
		}
	}
}
