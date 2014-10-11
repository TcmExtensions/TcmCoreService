#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Page
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
using TcmCoreService.Info;
using TcmCoreService.Interfaces;
using TcmCoreService.Misc;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="Page" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PageData" />.
	/// </summary>
	public class Page : VersionedItem, IWorkflowItem
	{
		private PageData mPageData;

		private ApprovalStatus mApprovalStatus = null;
		private IEnumerable<ComponentPresentation> mComponentPresentations = null;
		private PageTemplate mPageTemplate = null;
		private Schema mRegionSchema = null;
		private Info.Workflow mWorkflow = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Page"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="pageData"><see cref="T:Tridion.ContentManager.CoreService.Client.PageData" /></param>
		protected Page(Client client, PageData pageData): base(client, pageData)
		{
			if (pageData == null)
				throw new ArgumentNullException("pageData");

			mPageData = pageData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Page"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Page(Client client, TcmUri uri): this(client, client.Read<PageData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Page" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PageData" />
		/// </summary>
		/// <param name="pageData"><see cref="T:Tridion.ContentManager.CoreService.Client.PageData" /></param>
		protected void Reload(PageData pageData)
		{
			if (pageData == null)
				throw new ArgumentNullException("pageData");

			mPageData = pageData;
			base.Reload(pageData);

			mApprovalStatus = null;
			mComponentPresentations = null;
			mPageTemplate = null;
			mRegionSchema = null;
			mWorkflow = null;
		}

		/// <summary>
		/// Reload the <see cref="Page" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<PageData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Page" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<PageData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Page" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<PageData>(this.Id));
		}

		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="Page" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="Page" />
		/// </value>
		public ApprovalStatus ApprovalStatus
		{
			get
			{
				if (mApprovalStatus == null && mPageData.ApprovalStatus.IdRef != TcmUri.NullUri)
					mApprovalStatus = new ApprovalStatus(Client, mPageData.ApprovalStatus.IdRef);

				return mApprovalStatus;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.ComponentPresentation" /> for this <see cref="Page" />
		/// </summary>
		/// <value>
		/// <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.ComponentPresentation" /> for this <see cref="Page" />
		/// </value>
		public IEnumerable<ComponentPresentation> ComponentPresentations
		{
			get
			{
				if (mComponentPresentations == null && mPageData.ComponentPresentations != null)
					mComponentPresentations = mPageData.ComponentPresentations.Select(componentPresentation => new ComponentPresentation(Client, componentPresentation));

				return mComponentPresentations;
			}
			set
			{
				mComponentPresentations = value;
				
				if (value != null)
				{
					mPageData.ComponentPresentations = mComponentPresentations.Select(componentPresentation => new ComponentPresentationData()
					{
						Component = new LinkToComponentData()
						{
							IdRef = componentPresentation.Component.Id
						},
						ComponentTemplate = new LinkToComponentTemplateData()
						{
							IdRef = componentPresentation.ComponentTemplate.Id
						}
					}).ToArray();
				}
				else
					mPageData.ComponentPresentations = null;
			}
		}

		/// <summary>
		/// Gets or sets the filename of this <see cref="Page" />
		/// </summary>
		/// <value>
		/// Filename of this <see cref="Page" />
		/// </value>
		public String FileName
		{
			get
			{
				return mPageData.FileName;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPageData.FileName = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Page"/> is using an inherited <see cref="PageTemplate" />.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Page"/> is using an inherited <see cref="PageTemplate" />; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsPageTemplateInherited
		{
			get
			{
				return mPageData.IsPageTemplateInherited.GetValueOrDefault(false);
			}
			set
			{
				mPageData.IsPageTemplateInherited = value;
			}
		}

		/// <summary>
		/// Gets or sets <see cref="PageTemplate" /> for this <see cref="Page" />
		/// </summary>
		/// <value>
		/// <see cref="PageTemplate" /> for this <see cref="Page" />
		/// </value>
		public PageTemplate PageTemplate
		{
			get
			{
				if (mPageTemplate == null && mPageData.PageTemplate != null)
					mPageTemplate = new PageTemplate(Client, mPageData.PageTemplate.IdRef);

				return mPageTemplate;
			}
			set
			{
				mPageTemplate = value;

				if (value == null)
					mPageData.PageTemplate.IdRef = TcmUri.NullUri;
				else
					mPageData.PageTemplate.IdRef = mPageTemplate.Id;
			}
		}

        /// <summary>
        /// Gets or sets <see cref="PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Page" />
        /// </summary>
        /// <value>
        /// <see cref="PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Page" />
        /// </value>
        public TcmUri PageTemplateUri
        {
            get
            {
                return mPageData.PageTemplate.IdRef;
            }
            set
            {
                mPageTemplate = null;

                if (value == null)
                    mPageData.PageTemplate.IdRef = TcmUri.NullUri;
                else
                    mPageData.PageTemplate.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the <see cref="Page" /> Region <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Page" /> Region <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema RegionSchema
		{
			get
			{
				if (mRegionSchema == null && mPageData.RegionSchema.IdRef != TcmUri.NullUri)
					mRegionSchema = new Schema(Client, mPageData.RegionSchema.IdRef);

				return mRegionSchema;
			}
			set
			{
				mRegionSchema = value;

				if (value != null)
					mPageData.RegionSchema.IdRef = value.Id;
				else
					mPageData.RegionSchema.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets Region <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Page" />
        /// </summary>
        /// <value>
        /// Region <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Page" />
        /// </value>
        public TcmUri RegionSchemaUri
        {
            get
            {
                return mPageData.RegionSchema.IdRef;
            }
            set
            {
                mRegionSchema = null;

                if (value == null)
                    mPageData.RegionSchema.IdRef = TcmUri.NullUri;
                else
                    mPageData.RegionSchema.IdRef = value;
            }
        }

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="Page" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="Page" />
		/// </value>
		public Info.Workflow Workflow
		{
			get
			{
				if (mWorkflow == null && mPageData.WorkflowInfo != null)
					mWorkflow = new Info.Workflow(Client, mPageData.WorkflowInfo);

				return mWorkflow;
			}
		}
	}
}
