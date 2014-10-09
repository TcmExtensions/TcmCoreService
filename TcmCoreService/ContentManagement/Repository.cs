#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Repository
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
using TcmCoreService.Misc;
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="Repository" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" />.
	/// </summary>
	public class Repository : SystemWideObject
	{
		private RepositoryData mRepositoryData;
        private AccessControlList mAccessControlList = null;
        private Schema mDefaultMultimediaSchema = null;
        private Location mLocation = null;
        private IEnumerable<Repository> mParents = null;
        private Folder mRootFolder = null;
        private ProcessDefinition mTaskProcess = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="schemaData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" /></param>
		protected Repository(Client client, RepositoryData repositoryData): base(client, repositoryData)
		{
			if (repositoryData == null)
				throw new ArgumentNullException("repositoryData");

			mRepositoryData = repositoryData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Repository(Client client, TcmUri uri): this(client, client.Read<RepositoryData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Repository" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" />
		/// </summary>
		/// <param name="repositoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" /></param>
		protected void Reload(RepositoryData repositoryData)
		{
			if (repositoryData == null)
				throw new ArgumentNullException("repositoryData");

			mRepositoryData = repositoryData;
			base.Reload(repositoryData);

            mAccessControlList = null;
            mDefaultMultimediaSchema = null;
            mLocation = null;
            mParents = null;
            mRootFolder = null;
            mTaskProcess = null;
		}

		/// <summary>
		/// Reload the <see cref="Repository" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<RepositoryData>(this.Id));
		}

        /// <summary>
        /// Gets the <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="Repository" />
        /// </summary>
        /// <value>
        /// <see cref="T:TcmCoreService.Info.AccessControlList" /> for this <see cref="Repository" />
        /// </value>
        public AccessControlList AccessControlList
        {
            get
            {
                if (mAccessControlList == null)
                    mAccessControlList = new AccessControlList(Client, mRepositoryData.AccessControlList);

                return mAccessControlList;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Repository" /> categories Xsd
        /// </summary>
        /// <value>
        /// <see cref="Repository" /> categories Xsd
        /// </value>
        public String CategoriesXsd
        {
            get
            {
                return mRepositoryData.CategoriesXsd;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    mRepositoryData.CategoriesXsd = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Repository" /> default multimedia <see cref="T:TcmCoreService.ContentManagement.Schema" />
        /// </summary>
        /// <value>
        /// <see cref="Repository" /> default multimedia <see cref="T:TcmCoreService.ContentManagement.Schema" />
        /// </value>
        public Schema DefaultMultimediaSchema
        {
            get
            {
                if (mDefaultMultimediaSchema == null && mRepositoryData.DefaultMultimediaSchema.IdRef != TcmUri.NullUri)
                    mDefaultMultimediaSchema = new Schema(Client, mRepositoryData.DefaultMultimediaSchema.IdRef);

                return mDefaultMultimediaSchema;
            }
            set
            {
                mDefaultMultimediaSchema = value;

                if (value != null)
                    mRepositoryData.DefaultMultimediaSchema.IdRef = value.Id;
                else
                    mRepositoryData.DefaultMultimediaSchema.IdRef = TcmUri.NullUri;
            }
        }

        /// <summary>
        /// Gets a value indicating if the <see cref="Repository" /> has children
        /// </summary>
        /// <value>
        /// Indicates if the <see cref="Repository" /> has children
        /// </value>
        public Boolean HasChildren
        {
            get
            {
                return mRepositoryData.HasChildren.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Repository" /> key
        /// </summary>
        /// <value>
        /// <see cref="Repository" /> key
        /// </value>
        public String Key
        {
            get
            {
                return mRepositoryData.Key;
            }
            set
            {
                mRepositoryData.Key = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="Repository" /> <see cref="T:TcmCoreService.Info.Location" />
        /// </summary>
        /// <value>
        /// <see cref="Repository" /> <see cref="T:TcmCoreService.Info.Location" />
        /// </value>
        public Location Location
        {
            get
            {
                if (mLocation == null && mRepositoryData.LocationInfo != null)
                    mLocation = new Location(Client, mRepositoryData.LocationInfo);

                return mLocation;
            }
        }

		/// <summary>
		/// Gets or sets the <see cref="Repository" /> metadata Xml
		/// </summary>
		/// <value>
		/// <see cref="Repository" /> metadata Xml
		/// </value>
		public String Metadata
		{
			get
			{
				return mRepositoryData.Metadata;
			}
			set
			{
				mRepositoryData.Metadata = value;
			}
		}

		/// <summary>
		/// Gets or sets the metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// Metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema MetadataSchema
		{
			get
			{
				if (mRepositoryData.MetadataSchema.IdRef != TcmUri.NullUri)
					return new Schema(Client, mRepositoryData.MetadataSchema.IdRef);
				else
					return null;
			}
			set
			{
				if (value != null)
					mRepositoryData.MetadataSchema.IdRef = value.Id;
				else
					mRepositoryData.MetadataSchema.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Retrieves the list of parent <see cref="T:System.Collections.Generic.IEnumerable{Repository}" /> for this <see cref="Repository" />
        /// </summary>
        /// <value>
        /// List of parent <see cref="T:System.Collections.Generic.IEnumerable{Repository}" /> for this <see cref="Repository" />
        /// </value>
        public IEnumerable<Repository> Parents
        {
            get
            {
                if (mParents == null && mRepositoryData.Parents != null)
                    mParents = mRepositoryData.Parents.Select(keyword => new Repository(Client, keyword.IdRef));

                return mParents;
            }
            set
            {
                mParents = value;

                if (value != null)
                    mRepositoryData.Parents = value.Select(keyword => new LinkToRepositoryData()
                    {
                        IdRef = keyword.Id
                    }).ToArray();
                else
                    mRepositoryData.Parents = null;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Repository" /> <see cref="T:TcmCoreService.ContentManagement.Folder" />
        /// </summary>
        /// <value>
        /// <see cref="Repository" /> <see cref="T:TcmCoreService.ContentManagement.Folder" />
        /// </value>
        public Folder RootFolder
        {
			get
			{
				if (mRootFolder == null && mRepositoryData.RootFolder.IdRef != TcmUri.NullUri)
					mRootFolder = new Folder(Client, mRepositoryData.RootFolder.IdRef);

				return mRootFolder;
			}
			set
			{
				mRootFolder = value;

				if (value != null)
					mRepositoryData.RootFolder.IdRef = value.Id;
				else
					mRepositoryData.RootFolder.IdRef = TcmUri.NullUri;				
			}
        }

        /// <summary>
        /// Gets or sets the component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Repository" />
        /// </summary>
        /// <value>
        /// Component <see cref="T:TcmCoreService.Workflow.ProcessDefinition" /> for this <see cref="Repository" />
        /// </value>
        public ProcessDefinition TaskProcess
        {
            get
            {
                if (mTaskProcess == null && mRepositoryData.TaskProcess != null)
                    mTaskProcess = new TridionProcessDefinition(Client, mRepositoryData.TaskProcess.IdRef);

                return mTaskProcess;
            }
            set
            {
                mTaskProcess = value;

                if (value != null)
                    mRepositoryData.TaskProcess.IdRef = value.Id;
                else
                    mRepositoryData.TaskProcess.IdRef = TcmUri.NullUri;
            }
        }
	}
}
