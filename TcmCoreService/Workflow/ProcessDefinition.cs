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
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="processDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionData" /></param>
		protected ProcessDefinition(Client client, ProcessDefinitionData processDefinitionData): base(client, processDefinitionData)
		{
			if (processDefinitionData == null)
				throw new ArgumentNullException("processDefinitionData");

			mProcessDefinitionData = processDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ProcessDefinition(Client client, TcmUri uri): this(client, client.Read<ProcessDefinitionData>(uri))
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
			Reload(Client.Read<ProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="ProcessDefinition" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<ProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="ProcessDefinition" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<ProcessDefinitionData>(this.Id));
		}
	}
}
