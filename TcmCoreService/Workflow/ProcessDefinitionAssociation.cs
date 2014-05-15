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
	/// <see cref="ProcessDefinitionAssociation" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionAssociationData" />.
	/// </summary>
	public class ProcessDefinitionAssociation : IdentifiableObject
	{
		private ProcessDefinitionAssociationData mProcessDefinitionAssociationData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessDefinitionAssociation"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="processDefinitionAssociationData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionAssociationData" /></param>
		protected ProcessDefinitionAssociation(Session session, ProcessDefinitionAssociationData processDefinitionAssociationData): base(session, processDefinitionAssociationData)
		{
			if (processDefinitionAssociationData == null)
				throw new ArgumentNullException("processDefinitionAssociationData");

			mProcessDefinitionAssociationData = processDefinitionAssociationData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessDefinitionAssociation"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ProcessDefinitionAssociation(Session session, TcmUri uri): this(session, session.Read<ProcessDefinitionAssociationData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ProcessDefinitionAssociation" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionAssociationData" />
		/// </summary>
		/// <param name="processDefinitionAssociationData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessDefinitionAssociationData" /></param>
		protected void Reload(ProcessDefinitionAssociationData processDefinitionAssociationData)
		{
			if (processDefinitionAssociationData == null)
				throw new ArgumentNullException("processDefinitionAssociationData");

			mProcessDefinitionAssociationData = processDefinitionAssociationData;
			base.Reload(processDefinitionAssociationData);
		}

		/// <summary>
		/// Reload the <see cref="ProcessDefinitionAssociation" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<ProcessDefinitionAssociationData>(this.Id));
		}
	}
}
