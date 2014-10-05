#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: WorkflowObject
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
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="WorkflowObject" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowObjectData" />.
	/// </summary>
	public abstract class WorkflowObject : IdentifiableObject
	{
		private WorkflowObjectData mWorkflowObjectData;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowObject"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="workflowObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowObjectData" /></param>
		protected WorkflowObject(Client client, WorkflowObjectData workflowObjectData): base(client, workflowObjectData)
		{
			if (workflowObjectData == null)
				throw new ArgumentNullException("workflowObjectData");

			mWorkflowObjectData = workflowObjectData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowObject"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal WorkflowObject(Session session, TcmUri uri): this(session, session.Read<WorkflowObjectData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="WorkflowObject" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowObjectData" />
		/// </summary>
		/// <param name="workflowObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowObjectData" /></param>
		protected void Reload(WorkflowObjectData workflowObjectData)
		{
			if (workflowObjectData == null)
				throw new ArgumentNullException("workflowObjectData");

			mWorkflowObjectData = workflowObjectData;
			base.Reload(workflowObjectData);
		}

		/// <summary>
		/// Reload the <see cref="WorkflowObject" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<WorkflowObjectData>(this.Id));
		}
	}
}
