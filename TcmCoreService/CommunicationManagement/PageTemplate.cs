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
using TcmCoreService.ContentManagement;
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
        private Schema mPageSchema = null;
		private Info.Workflow mWorkflow = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageTemplate"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="pageTemplateData"><see cref="T:Tridion.ContentManager.CoreService.Client.PageTemplateData" /></param>
		protected PageTemplate(Client client, PageTemplateData pageTemplateData): base(client, pageTemplateData)
		{
			if (pageTemplateData == null)
				throw new ArgumentNullException("pageTemplateData");

			mPageTemplateData = pageTemplateData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PageTemplate"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal PageTemplate(Client client, TcmUri uri): this(client, client.Read<PageTemplateData>(uri))
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
            mPageSchema = null;
			mWorkflow = null;
		}

		/// <summary>
		/// Reload the <see cref="PageTemplate" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<PageTemplateData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="PageTemplate" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<PageTemplateData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="PageTemplate" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<PageTemplateData>(this.Id));
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
					mApprovalStatus = new ApprovalStatus(Client, mPageTemplateData.ApprovalStatus.IdRef);

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
        /// Gets or sets the <see cref="PageTemplate" /> Page <see cref="T:TcmCoreService.ContentManagement.Schema" />
        /// </summary>
        /// <value>
        /// <see cref="PageTemplate" /> Page <see cref="T:TcmCoreService.ContentManagement.Schema" />
        /// </value>
        public Schema PageSchema
        {
            get
            {
                if (mPageTemplateData == null && mPageTemplateData.PageSchema.IdRef != TcmUri.NullUri)
                    mPageSchema = new Schema(Client, mPageTemplateData.PageSchema.IdRef);

                return mPageSchema;
            }
            set
            {
                mPageSchema = value;

                if (value != null)
                    mPageTemplateData.PageSchema.IdRef = value.Id;
                else
                    mPageTemplateData.PageSchema.IdRef = TcmUri.NullUri;
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
					mWorkflow = new Info.Workflow(Client, mPageTemplateData.WorkflowInfo);

				return mWorkflow;				
			}
		}
	}
}
