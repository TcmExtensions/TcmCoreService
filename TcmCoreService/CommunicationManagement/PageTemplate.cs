#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Page Template
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
using TcmCoreService.Interfaces;
using TcmCoreService.Misc;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="PageTemplate" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PageTemplateData" />.
	/// </summary>
	public class PageTemplate : Template, IWorkflowItem
	{
		private PageTemplateData mPageTemplateData;

		private ApprovalStatus mApprovalStatus = null;
		private Info.Workflow mWorkflow = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageTemplate"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="pageTemplateData"><see cref="T:Tridion.ContentManager.CoreService.Client.PageTemplateData" /></param>
		protected PageTemplate(Session session, PageTemplateData pageTemplateData): base(session, pageTemplateData)
		{
			if (pageTemplateData == null)
				throw new ArgumentNullException("pageTemplateData");

			mPageTemplateData = pageTemplateData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageTemplate"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal PageTemplate(Session session, TcmUri uri): this(session, session.Read<PageTemplateData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="PageTemplate" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PageTemplateData" />
		/// </summary>
		/// <param name="pageTemplateData"><see cref="T:Tridion.ContentManager.CoreService.Client.PageTemplateData" /></param>
		protected void Reload(PageTemplateData pageTemplateData)
		{
			if (pageTemplateData == null)
				throw new ArgumentNullException("pageTemplateData");

			mPageTemplateData = pageTemplateData;
			base.Reload(pageTemplateData);

			mApprovalStatus = null;
			mWorkflow = null;
		}

		/// <summary>
		/// Reload the <see cref="PageTemplate" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<PageTemplateData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="PageTemplate" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<PageTemplateData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="PageTemplate" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<PageTemplateData>(this.Id));
		}

		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="PageTemplate" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="PageTemplate" />
		/// </value>
		public ApprovalStatus ApprovalStatus
		{
			get
			{
				if (mApprovalStatus == null && mPageTemplateData.ApprovalStatus.IdRef != TcmUri.NullUri)
					mApprovalStatus = new ApprovalStatus(Session, mPageTemplateData.ApprovalStatus.IdRef);

				return mApprovalStatus;
			}
		}

		/// <summary>
		/// Gets or sets the file extension for this <see cref="PageTemplate" />
		/// </summary>
		/// <value>
		/// The file extension for this <see cref="PageTemplate" />
		/// </value>
		public String FileExtension
		{
			get
			{
				return mPageTemplateData.FileExtension;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPageTemplateData.FileExtension = value;
			}
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="PageTemplate" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="PageTemplate" />
		/// </value>
		public Info.Workflow Workflow
		{
			get
			{
				if (mWorkflow == null && mPageTemplateData.WorkflowInfo != null)
					mWorkflow = new Info.Workflow(Session, mPageTemplateData.WorkflowInfo);

				return mWorkflow;				
			}
		}
	}
}
