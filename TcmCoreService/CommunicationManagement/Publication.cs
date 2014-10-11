#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Publication
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
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="Publication" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PublicationData" />.
	/// </summary>
	public class Publication : Repository
	{
		private PublicationData mPublicationData;

		private ComponentTemplate mComponentSnapshotTemplate = null;
		private ProcessDefinition mComponentTemplateProcess = null;
		private ComponentTemplate mDefaultComponentTemplate = null;
		private PageTemplate mDefaultPageTemplate = null;
		private TemplateBuildingBlock mDefaultTemplateBuildingBlock = null;
		private PageTemplate mPageSnapshotTemplate = null;
		private ProcessDefinition mPageTemplateProcess = null;
        private ProcessDefinition mTemplateBundleProcess = null;
		private StructureGroup mRootStructureGroup = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Publication"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="publicationData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublicationData" /></param>
		protected Publication(Client client, PublicationData publicationData): base(client, publicationData)
		{
			if (publicationData == null)
				throw new ArgumentNullException("publicationData");

			mPublicationData = publicationData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Publication"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Publication(Client client, TcmUri uri): this(client, client.Read<PublicationData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Publication" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PublicationData" />
		/// </summary>
		/// <param name="publicationData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublicationData" /></param>
		protected void Reload(PublicationData publicationData)
		{
			if (publicationData == null)
				throw new ArgumentNullException("publicationData");

			mPublicationData = publicationData;
			base.Reload(publicationData);

			mComponentSnapshotTemplate = null;
			mComponentTemplateProcess = null;
			mDefaultComponentTemplate = null;
			mDefaultPageTemplate = null;
			mDefaultTemplateBuildingBlock = null;
			mPageSnapshotTemplate = null;
			mPageTemplateProcess = null;
            mTemplateBundleProcess = null;
			mRootStructureGroup = null;
		}

		/// <summary>
		/// Reload the <see cref="Publication" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<PublicationData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the component snapshot <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Component snapshot <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> for this <see cref="Publication" />
		/// </value>
		public ComponentTemplate ComponentSnapshotTemplate
		{
			get
			{
				if (mComponentSnapshotTemplate == null && mPublicationData.ComponentSnapshotTemplate != null)
					mComponentSnapshotTemplate = new ComponentTemplate(Client, mPublicationData.ComponentSnapshotTemplate.IdRef);

				return mComponentSnapshotTemplate;
			}
			set
			{
				mComponentSnapshotTemplate = value;

				if (value != null)
					mPublicationData.ComponentSnapshotTemplate.IdRef = value.Id;
				else
					mPublicationData.ComponentSnapshotTemplate.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets the component snapshot <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Component snapshot <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri ComponentSnapshotTemplateUri
        {
            get
            {
                return mPublicationData.ComponentSnapshotTemplate.IdRef;
            }
            set
            {
                mComponentSnapshotTemplate = null;

                if (value == null)
                    mPublicationData.ComponentSnapshotTemplate.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.ComponentSnapshotTemplate.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the component template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Component template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
		/// </value>
		public ProcessDefinition ComponentTemplateProcess
		{
			get
			{
				if (mComponentTemplateProcess == null && mPublicationData.ComponentTemplateProcess != null)
					mComponentTemplateProcess = new TridionProcessDefinition(Client, mPublicationData.ComponentTemplateProcess.IdRef);

				return mComponentTemplateProcess;
			}
			set
			{
				mComponentTemplateProcess = value;

				if (value != null)
					mPublicationData.ComponentTemplateProcess.IdRef = value.Id;
				else
					mPublicationData.ComponentTemplateProcess.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets the component template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Component template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri ComponentTemplateProcessUri
        {
            get
            {
                return mPublicationData.ComponentTemplateProcess.IdRef;
            }
            set
            {
                mComponentTemplateProcess = null;

                if (value == null)
                    mPublicationData.ComponentTemplateProcess.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.ComponentTemplateProcess.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Default <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> for this <see cref="Publication" />
		/// </value>
		public ComponentTemplate DefaultComponentTemplate
		{
			get
			{
				if (mDefaultComponentTemplate == null && mPublicationData.DefaultComponentTemplate != null)
					mDefaultComponentTemplate = new ComponentTemplate(Client, mPublicationData.DefaultComponentTemplate.IdRef);

				return mDefaultComponentTemplate;
			}
			set
			{
				mDefaultComponentTemplate = value;

				if (value != null)
					mPublicationData.DefaultComponentTemplate.IdRef = value.Id;
				else
					mPublicationData.DefaultComponentTemplate.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Default <see cref="T:TcmCoreService.CommunicationManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri DefaultComponentTemplateUri
        {
            get
            {
                return mPublicationData.DefaultComponentTemplate.IdRef;
            }
            set
            {
                mDefaultComponentTemplate = null;

                if (value == null)
                    mPublicationData.DefaultComponentTemplate.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.DefaultComponentTemplate.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="Publication" />
		/// </value>
		public PageTemplate DefaultPageTemplate
		{
			get
			{
				if (mDefaultPageTemplate == null && mPublicationData.DefaultPageTemplate != null)
					mDefaultPageTemplate = new PageTemplate(Client, mPublicationData.DefaultPageTemplate.IdRef);

				return mDefaultPageTemplate;
			}
			set
			{
				mDefaultPageTemplate = value;

				if (value != null)
					mPublicationData.DefaultPageTemplate.IdRef = value.Id;
				else
					mPublicationData.DefaultPageTemplate.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri DefaultPageTemplateUri
        {
            get
            {
                return mPublicationData.DefaultPageTemplate.IdRef;
            }
            set
            {
                mDefaultPageTemplate = null;

                if (value == null)
                    mPublicationData.DefaultPageTemplate.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.DefaultPageTemplate.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Default <see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /> for this <see cref="Publication" />
		/// </value>
		public TemplateBuildingBlock DefaultTemplateBuildingBlock
		{
			get
			{
				if (mDefaultTemplateBuildingBlock == null && mPublicationData.DefaultTemplateBuildingBlock != null)
					mDefaultTemplateBuildingBlock = new TemplateBuildingBlock(Client, mPublicationData.DefaultTemplateBuildingBlock.IdRef);

				return mDefaultTemplateBuildingBlock;
			}
			set
			{
				mDefaultTemplateBuildingBlock = value;

				if (value != null)
					mPublicationData.DefaultTemplateBuildingBlock.IdRef = value.Id;
				else
					mPublicationData.DefaultTemplateBuildingBlock.IdRef = TcmUri.NullUri;				
			}
		}

        /// <summary>
        /// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Default <see cref="T:TcmCoreService.CommunicationManagement.TemplateBuildingBlock" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri DefaultTemplateBuildingBlockUri
        {
            get
            {
                return mPublicationData.DefaultTemplateBuildingBlock.IdRef;
            }
            set
            {
                mDefaultTemplateBuildingBlock = null;

                if (value == null)
                    mPublicationData.DefaultTemplateBuildingBlock.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.DefaultTemplateBuildingBlock.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> multimedia path.
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> multimedia path.
		/// </value>
		public String MultimediaPath
		{
			get
			{
				return mPublicationData.MultimediaPath;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublicationData.MultimediaPath = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> multimedia url.
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> multimedia url.
		/// </value>
		public String MultimediaUrl
		{
			get
			{
				return mPublicationData.MultimediaUrl;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublicationData.MultimediaUrl = value;
			}
		}

		/// <summary>
		/// Gets or sets the page snapshot <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Page snapshot <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="Publication" />
		/// </value>
		public PageTemplate PageSnapshotTemplate
		{
			get
			{
				if (mPageSnapshotTemplate == null && mPublicationData.PageSnapshotTemplate != null)
					mPageSnapshotTemplate = new PageTemplate(Client, mPublicationData.PageSnapshotTemplate.IdRef);

				return mPageSnapshotTemplate;
			}
			set
			{
				mPageSnapshotTemplate = value;

				if (value != null)
					mPublicationData.PageSnapshotTemplate.IdRef = value.Id;
				else
					mPublicationData.PageSnapshotTemplate.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Gets or sets the page snapshot <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Page snapshot <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri PageSnapshotTemplateUri
        {
            get
            {
                return mPublicationData.PageSnapshotTemplate.IdRef;
            }
            set
            {
                mPageSnapshotTemplate = null;

                if (value == null)
                    mPublicationData.PageSnapshotTemplate.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.PageSnapshotTemplate.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the page template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
		/// </summary>
		/// <value>
		/// Page template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
		/// </value>
		public ProcessDefinition PageTemplateProcess
		{
			get
			{
				if (mPageTemplateProcess == null && mPublicationData.PageTemplateProcess != null)
					mPageTemplateProcess = new TridionProcessDefinition(Client, mPublicationData.PageTemplateProcess.IdRef);

				return mPageTemplateProcess;
			}
			set
			{
				mPageTemplateProcess = value;

				if (value != null)
					mPublicationData.PageTemplateProcess.IdRef = value.Id;
				else
					mPublicationData.PageTemplateProcess.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Gets or sets the page template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Page template <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri PageTemplateProcessUri
        {
            get
            {
                return mPublicationData.PageTemplateProcess.IdRef;
            }
            set
            {
                mPageTemplateProcess = null;

                if (value == null)
                    mPublicationData.PageTemplateProcess.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.PageTemplateProcess.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> path.
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> path.
		/// </value>
		public String PublicationPath
		{
			get
			{
				return mPublicationData.PublicationPath;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublicationData.PublicationPath = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> type.
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> type.
		/// </value>
		public String PublicationType
		{
			get
			{
				return mPublicationData.PublicationType;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublicationData.PublicationType = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> url.
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> url.
		/// </value>
		public String PublicationUrl
		{
			get
			{
				return mPublicationData.PublicationUrl;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublicationData.PublicationUrl = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Publication" /> root <see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" />
		/// </summary>
		/// <value>
		/// <see cref="Publication" /> root <see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" />
		/// </value>
		public StructureGroup RootStructureGroup
		{
			get
			{
				if (mRootStructureGroup == null && mPublicationData.RootStructureGroup != null)
					mRootStructureGroup = new StructureGroup(Client, mPublicationData.RootStructureGroup.IdRef);

				return mRootStructureGroup;
			}
			set
			{
				mRootStructureGroup = value;

				if (value != null)
					mPublicationData.RootStructureGroup.IdRef = mRootStructureGroup.Id;
				else
					mPublicationData.RootStructureGroup.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="Publication" /> root <see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </summary>
        /// <value>
        /// <see cref="Publication" /> root <see cref="T:TcmCoreService.CommunicationManagement.StructureGroup" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </value>
        public TcmUri RootStructureGroupUri
        {
            get
            {
                return mPublicationData.RootStructureGroup.IdRef;
            }
            set
            {
                mRootStructureGroup = null;

                if (value == null)
                    mPublicationData.RootStructureGroup.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.RootStructureGroup.IdRef = value;
            }
        }

		/// <summary>
		/// Gets or sets a value indicating whether to share process associations for this <see cref="Publicaiton" />
		/// </summary>
		/// <value>
		/// <c>true</c> if sharing process associations; otherwise, <c>false</c>.
		/// </value>
		public Boolean ShareProcessAssociations
		{
			get
			{
				return mPublicationData.ShareProcessAssociations.GetValueOrDefault(false);
			}
			set
			{
				mPublicationData.ShareProcessAssociations = value;
			}
		}

        /// <summary>
        /// Gets or sets the template bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Template bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Publication" />
        /// </value>
        public ProcessDefinition TemplateBundleProcess
        {
            get
            {
                if (mTemplateBundleProcess == null && mPublicationData.TemplateBundleProcess != null)
                    mTemplateBundleProcess = new TridionProcessDefinition(Client, mPublicationData.TemplateBundleProcess.IdRef);

                return mTemplateBundleProcess;
            }
            set
            {
                mTemplateBundleProcess = value;

                if (value != null)
                    mPublicationData.TemplateBundleProcess.IdRef = value.Id;
                else
                    mPublicationData.TemplateBundleProcess.IdRef = TcmUri.NullUri;
            }
        }

        /// <summary>
        /// Gets or sets the template bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </summary>
        /// <value>
        /// Template bundle <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="Publication" />
        /// </value>
        public TcmUri TemplateBundleProcessUri
        {
            get
            {
                return mPublicationData.TemplateBundleProcess.IdRef;
            }
            set
            {
                mTemplateBundleProcess = null;

                if (value == null)
                    mPublicationData.TemplateBundleProcess.IdRef = TcmUri.NullUri;
                else
                    mPublicationData.TemplateBundleProcess.IdRef = value;
            }
        }
	}
}
