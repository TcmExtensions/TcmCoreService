#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Workflow
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
using TcmCoreService.Security;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="Workflow" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowInfo" />
	/// </summary>
	public class Workflow : InfoBase
	{
		private WorkflowInfo mWorkflowInfo;
		private ActivityInstance mActivityInstance = null;
		private ProcessInstance mProcessInstance = null;
		private User mAssignee = null;
		private User mPerformer = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Workflow" /> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="workflowInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.WorkflowInfo" /></param>
		internal Workflow(Client client, WorkflowInfo workflowInfo): base(client)
		{
			mWorkflowInfo = workflowInfo;
		}

		/// <summary>
		/// Gets the <see cref="Workflow" /> activity definition description.
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> activity definition description.
		/// </value>
		public String ActivityDefinitionDescription
		{
			get
			{
				return mWorkflowInfo.ActivityDefinitionDescription;
			}
		}

		/// <summary>
		/// Gets the <see cref="Workflow" /> previous message
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> previous message
		/// </value>
		public String PreviousMessage
		{
			get
			{
				return mWorkflowInfo.PreviousMessage;
			}
		}

		/// <summary>
		/// Gets <see cref="Component" /> <see cref="T:TcmCoreService.Workflow.ActivityInstance" />
		/// </summary>
		/// <value>
		/// <see cref="Component" /> <see cref="T:TcmCoreService.Workflow.ActivityInstance" />
		/// </value>
		public ActivityInstance ActivityInstance
		{
			get
			{
				if (mActivityInstance == null)
					mActivityInstance = new ActivityInstance(Client, mWorkflowInfo.ActivityInstance.IdRef);

				return mActivityInstance;
			}
		}

		/// <summary>
		/// Gets <see cref="Workflow" /> <see cref="T:TcmCoreService.Workflow.ProcessInstance" />
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> <see cref="T:TcmCoreService.Workflow.ProcessInstance" />
		/// </value>
		public ProcessInstance ProcessInstance
		{
			get
			{
				if (mProcessInstance == null)
					mProcessInstance = new ProcessInstance(Client, mWorkflowInfo.ProcessInstance.IdRef);

				return mProcessInstance;
			}
		}

		/// <summary>
		/// Gets <see cref="Component" /> <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityState" />
		/// </summary>
		/// <value>
		/// <see cref="Component" /> <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityState" />
		/// </value>
		public ActivityState ActivityState
		{
			get
			{
				return mWorkflowInfo.ActivityState.GetValueOrDefault(ActivityState.UnknownByClient);
			}
		}

		/// <summary>
		/// Gets <see cref="Workflow" /> assignee <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> assignee <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User Assignee
		{
			get
			{
				if (mAssignee == null)
					mAssignee = new User(Client, mWorkflowInfo.Assignee.IdRef);

				return mAssignee;
			}
		}

		/// <summary>
		/// Gets <see cref="Workflow" /> performer <see cref="T:TcmCoreService.Security.User" />
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> performer <see cref="T:TcmCoreService.Security.User" />
		/// </value>
		public User Performer
		{
			get
			{
				if (mPerformer == null)
					mPerformer = new User(Client, mWorkflowInfo.Performer.IdRef);

				return mPerformer;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="Workflow" /> creation date
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> creation date or <see cref="M:System.DateTime.MinValue" />
		/// </value>
		public DateTime CreationDate
		{
			get
			{
				return mWorkflowInfo.CreationDate.GetValueOrDefault(DateTime.MinValue);				
			}
		}

		/// <summary>
		/// Retrieves the <see cref="Workflow" /> start date
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> start date or <see cref="M:System.DateTime.MinValue" />
		/// </value>
		public DateTime StartDate
		{
			get
			{
				return mWorkflowInfo.StartDate.GetValueOrDefault(DateTime.MinValue);
			}
		}

		/// <summary>
		/// Retrieves the <see cref="Workflow" /> finish date
		/// </summary>
		/// <value>
		/// <see cref="Workflow" /> finish date or <see cref="M:System.DateTime.MinValue" />
		/// </value>
		public DateTime FinishDate
		{
			get
			{
				return mWorkflowInfo.FinishDate.GetValueOrDefault(DateTime.MinValue);				
			}
		}			
	}
}
