#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Component Template
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
	/// <see cref="ComponentTemplate" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentTemplateData" />.
	/// </summary>
	public class ComponentTemplate : Template, IWorkflowItem
	{
		private ComponentTemplateData mComponentTemplateData;

		private ApprovalStatus mApprovalStatus = null;

		private IEnumerable<Schema> mRelatedSchemas = null;
		private IEnumerable<Category> mTrackingCategories = null;		
		private Info.Workflow mWorkflow = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentTemplate"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="componentTemplateData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentTemplateData" /></param>
		protected ComponentTemplate(Session session, ComponentTemplateData componentTemplateData): base(session, componentTemplateData)
		{
			if (componentTemplateData == null)
				throw new ArgumentNullException("componentTemplateData");

			mComponentTemplateData = componentTemplateData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentTemplate"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ComponentTemplate(Session session, TcmUri uri): this(session, session.Read<ComponentTemplateData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ComponentTemplate" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentTemplateData" />
		/// </summary>
		/// <param name="componentTemplateData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentTemplateData" /></param>
		protected void Reload(ComponentTemplateData componentTemplateData)
		{
			if (componentTemplateData == null)
				throw new ArgumentNullException("componentTemplateData");

			mComponentTemplateData = componentTemplateData;
			base.Reload(componentTemplateData);

			mApprovalStatus = null;
			mRelatedSchemas = null;
			mTrackingCategories = null;
			mWorkflow = null;			
		}

		/// <summary>
		/// Reload the <see cref="ComponentTemplate" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<ComponentTemplateData>(Id));
		}

		/// <summary>
		/// Localize this <see cref="ComponentTemplate" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<ComponentTemplateData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="ComponentTemplate" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<ComponentTemplateData>(this.Id));
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ComponentTemplate" /> is allowed on a page.
		/// </summary>
		/// <value>
		///   <c>true</c> if this <see cref="ComponentTemplate" /> is allowed on a page; otherwise, <c>false</c>.
		/// </value>
		public Boolean AllowOnPage
		{
			get
			{
				return mComponentTemplateData.AllowOnPage.GetValueOrDefault(false);
			}
			set
			{
				mComponentTemplateData.AllowOnPage = value;				
			}
		}

		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="ComponentTemplate" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="ComponentTemplate" />
		/// </value>
		public ApprovalStatus ApprovalStatus
		{
			get
			{
				if (mApprovalStatus == null && mComponentTemplateData.ApprovalStatus.IdRef != TcmUri.NullUri)
					mApprovalStatus = new ApprovalStatus(Session, mComponentTemplateData.ApprovalStatus.IdRef);

				return mApprovalStatus;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ComponentTemplate" /> dynamic template.
		/// </summary>
		/// <value>
		/// <see cref="ComponentTemplate" /> dynamic template.
		/// </value>
		public String DynamicTemplate
		{
			get
			{
				return mComponentTemplateData.DynamicTemplate;
			}
			set
			{
				mComponentTemplateData.DynamicTemplate = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ComponentTemplate" /> output format.
		/// </summary>
		/// <value>
		/// <see cref="ComponentTemplate" /> output format.
		/// </value>
		public String OutputFormat
		{
			get
			{
				return mComponentTemplateData.OutputFormat;
			}
			set
			{
				mComponentTemplateData.OutputFormat = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TemplatePriority" />.
		/// </summary>
		/// <value>
		/// <see cref="ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TemplatePriority" />.
		/// </value>
		public TemplatePriority Priority
		{
			get
			{
				return (TemplatePriority)mComponentTemplateData.Priority.GetValueOrDefault(0);
			}
			set
			{
				mComponentTemplateData.Priority = (int)value;
			}
		}

		/// <summary>
		/// Gets or sets related <see cref="I:System.Collections.Generic{Schema}" /> for this <see cref="ComponentTemplate" />
		/// </summary>
		/// <value>
		/// Related <see cref="I:System.Collections.Generic{Schema}" /> for this <see cref="ComponentTemplate" />
		/// </value>
		public IEnumerable<Schema> RelatedSchemas
		{
			get
			{
				if (mRelatedSchemas == null && mComponentTemplateData.RelatedSchemas != null)
					mRelatedSchemas = mComponentTemplateData.RelatedSchemas.Select(schema => new Schema(Session, schema.IdRef));

				return mRelatedSchemas;
			}
			set
			{
				mRelatedSchemas = value;

				if (value != null)
				{
					mComponentTemplateData.RelatedSchemas = value.Select(schema => new LinkToSchemaData()
					{
						IdRef = schema.Id
					}).ToArray();
				}
				else
					mComponentTemplateData.RelatedSchemas = null;
			}
		}

		/// <summary>
		/// Gets or sets tracking <see cref="I:System.Collections.Generic{Category}" /> for this <see cref="ComponentTemplate" />
		/// </summary>
		/// <value>
		/// Tracking <see cref="I:System.Collections.Generic{Category}" /> for this <see cref="ComponentTemplate" />
		/// </value>
		public IEnumerable<Category> TrackingCategories
		{
			get
			{
				if (mTrackingCategories == null && mComponentTemplateData.TrackingCategories != null)
					mTrackingCategories = mComponentTemplateData.TrackingCategories.Select(category => new Category(Session, category.IdRef));

				return mTrackingCategories;
			}
			set				
			{
				mTrackingCategories = value;

				if (value != null)
				{
					mComponentTemplateData.TrackingCategories = value.Select(category => new LinkToCategoryData()
					{
						IdRef = category.Id
					}).ToArray();
				}
				else
					mComponentTemplateData.TrackingCategories = null;
			}
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="ComponentTemplate" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="ComponentTemplate" />
		/// </value>
		public Info.Workflow Workflow
		{
			get
			{
				if (mWorkflow == null && mComponentTemplateData.WorkflowInfo != null)
					mWorkflow = new Info.Workflow(Session, mComponentTemplateData.WorkflowInfo);

				return mWorkflow;
			}
		}
	}
}
