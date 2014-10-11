#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Component
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
using TcmCoreService.Info;
using TcmCoreService.Interfaces;
using TcmCoreService.Misc;
using TcmCoreService.Security;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="Component" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentData" />.
	/// </summary>
	public class Component : VersionedItem, IWorkflowItem
	{
		private ComponentData mComponentData;

		private Schema mSchema = null;
		private BinaryContent mBinaryContent = null;

		private ApprovalStatus mApprovalStatus = null;
		private Info.Workflow mWorkflow = null;		
				
		/// <summary>
		/// Initializes a new instance of the <see cref="Component"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="componentData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentData" /></param>
		protected Component(Client client, ComponentData componentData): base(client, componentData)
		{
			if (componentData == null)
				throw new ArgumentNullException("componentData");

			mComponentData = componentData;			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Component"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Component(Client client, TcmUri uri): this(client, client.Read<ComponentData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Component" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentData" />
		/// </summary>
		/// <param name="componentData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentData" /></param>
		protected void Reload(ComponentData componentData)
		{
			if (componentData == null)
				throw new ArgumentNullException("componentData");

			mComponentData = componentData;
			base.Reload(componentData);

			mApprovalStatus = null;
			mSchema = null;

			mBinaryContent = null;
			mWorkflow = null;
		}

		/// <summary>
		/// Reload the <see cref="Component" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ComponentData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Component" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<ComponentData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Component" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<ComponentData>(this.Id));
		}

		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="Component" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="Component" />
		/// </value>
		public ApprovalStatus ApprovalStatus
		{
			get
			{
				if (mApprovalStatus == null && mComponentData.ApprovalStatus.IdRef != TcmUri.NullUri)
					mApprovalStatus = new ApprovalStatus(Client, mComponentData.ApprovalStatus.IdRef);

				return mApprovalStatus;				
			}
		}

		/// <summary>
		/// Retrieves the <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentType" /> of this <see cref="Component" />
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentType" /> of this <see cref="Component" />
		/// </value>
		public ComponentType ComponentType
		{
			get
			{
				return mComponentData.ComponentType.GetValueOrDefault(ComponentType.Normal);				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Component" /> content Xml
		/// </summary>
		/// <value>
		/// <see cref="Component" /> content Xml
		/// </value>
		public String Content
		{
			get
			{
				return mComponentData.Content;
			}
			set
			{
				mComponentData.Content = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Component" /> <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Component" /> <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema Schema
		{
			get
			{
				if (mSchema == null && mComponentData.Schema.IdRef != TcmUri.NullUri)
					mSchema = new Schema(Client, mComponentData.Schema.IdRef);

				return mSchema;
			}
			set
			{
				mSchema = value;

				if (value != null)
					mComponentData.Schema.IdRef = value.Id;
				else
					mComponentData.Schema.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="Component" /> <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </summary>
        /// <value>
        /// <see cref="Component" /> <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </value>
        public TcmUri SchemaUri
        {
            get
            {
                return mComponentData.Schema.IdRef;
            }
            set
            {
                mSchema = null;

                if (value == null)
                    mComponentData.Schema.IdRef = TcmUri.NullUri;
                else
                    mComponentData.Schema.IdRef = value;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this <see cref="Component" /> is based on on a mandatory schema.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Component" /> is based on mandatory schema; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsBasedOnMandatorySchema
		{
			get
			{
				return mComponentData.IsBasedOnMandatorySchema.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Component" /> is based on a Tridion web schema.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Component" /> is based on a Tridion web schema; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsBasedOnTridionWebSchema
		{
			get
			{
				return mComponentData.IsBasedOnTridionWebSchema.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.BinaryContent" /> for this <see cref="Component" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.BinaryContent" /> for this <see cref="Component" />
		/// </value>
		public BinaryContent BinaryContent
		{
			get
			{
				if (mBinaryContent == null && mComponentData.BinaryContent != null)
					mBinaryContent = new BinaryContent(Client, mComponentData.BinaryContent);

				return mBinaryContent;
			}
			set
			{
				mBinaryContent = value;
				mComponentData.BinaryContent = mBinaryContent.BinaryContentData;
			}
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="Component" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="Component" />
		/// </value>
		public Info.Workflow Workflow
		{
			get
			{
				if (mWorkflow == null && mComponentData.WorkflowInfo != null)
					mWorkflow = new Info.Workflow(Client, mComponentData.WorkflowInfo);
				
				return mWorkflow;
			}
		}
	}
}
