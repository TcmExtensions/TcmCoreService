#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Blueprint
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
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="BluePrint" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintInfo" />
	/// </summary>
	public class BluePrint : InfoBase
	{
		private BluePrintInfo mBluePrintInfo;
        private Repository mOwningRepository = null;
        private RepositoryLocalObject mPrimaryBluePrintParentItem = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="BluePrint"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="bluePrintInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintInfo" /></param>
		public BluePrint(Client client, BluePrintInfo bluePrintInfo): base(client)
		{
			mBluePrintInfo = bluePrintInfo;
		}

		/// <summary>
		/// Gets the owning repository.
		/// </summary>
		/// <value>
		/// The owning <see cref="T:TcmCoreService.ContentManagement.Repository" />.
		/// </value>
		public Repository OwningRepository
		{
			get
			{
				if (mOwningRepository == null)
					mOwningRepository = new Repository(Client, mBluePrintInfo.OwningRepository.IdRef);

				return mOwningRepository;
			}
		}

        /// <summary>
        /// Gets the owning repository <see cref="T:TcmCoreService.Misc.TcmUri" />.
        /// </summary>
        /// <value>
        /// The owning <see cref="T:TcmCoreService.ContentManagement.Repository" /> <see cref="T:TcmCoreService.Misc.TcmUri" />.
        /// </value>
        public TcmUri OwningRepositoryUri
        {
            get
            {
                return mBluePrintInfo.OwningRepository.IdRef;
            }
        }

        /// <summary>
        /// Gets the primary blueprint parent item.
        /// </summary>
        /// <value>
        /// The primary blueprint parent item <see cref="T:TcmCoreService.ContentManagement.RepositoryLocalObject" />.
        /// </value>
        public RepositoryLocalObject PrimaryBluePrintParentItem
        {
            get
            {
                if (mPrimaryBluePrintParentItem == null)
                {
                    TcmUri uri = mBluePrintInfo.PrimaryBluePrintParentItem.IdRef;

                    switch (uri.ItemType)
                    {
                        case (int)ItemType.Folder:
                            mPrimaryBluePrintParentItem = new Folder(Client, uri);
                            break;
                        case (int)ItemType.VirtualFolder:
                            mPrimaryBluePrintParentItem = new VirtualFolder(Client, uri);
                            break;
                        case (int)ItemType.Category:
                            mPrimaryBluePrintParentItem = new Category(Client, uri);
                            break;
                        case (int)ItemType.StructureGroup:
                            mPrimaryBluePrintParentItem = new StructureGroup(Client, uri);
                            break;
                        case (int)ItemType.Component:
                            mPrimaryBluePrintParentItem = new Component(Client, uri);
                            break;
                        case (int)ItemType.ComponentTemplate:
                            mPrimaryBluePrintParentItem = new ComponentTemplate(Client, uri);
                            break;
                        case (int)ItemType.Schema:
                            mPrimaryBluePrintParentItem = new Schema(Client, uri);
                            break;
                        case (int)ItemType.Keyword:
                            mPrimaryBluePrintParentItem = new Keyword(Client, uri);
                            break;
                        case (int)ItemType.PageTemplate:
                            mPrimaryBluePrintParentItem = new PageTemplate(Client, uri);
                            break;
                        case (int)ItemType.TemplateBuildingBlock:
                            mPrimaryBluePrintParentItem = new TemplateBuildingBlock(Client, uri);
                            break;
                    }
                }

                return mPrimaryBluePrintParentItem;
            }
        }

        /// <summary>
        /// Gets the primary blueprint parent item <see cref="T:TcmCoreService.Misc.TcmUri" />.
        /// </summary>
        /// <value>
        /// The primary blueprint parent item <see cref="T:TcmCoreService.ContentManagement.RepositoryLocalObject" /> <see cref="T:TcmCoreService.Misc.TcmUri" />.
        /// </value>
        public TcmUri PrimaryBluePrintParentItemUri
        {
            get
            {
                return mBluePrintInfo.PrimaryBluePrintParentItem.IdRef;
            }
        }

		/// <summary>
		/// Gets a value indicating whether this <see cref="RepositoryLocalObject" /> is shared.
		/// </summary>
		/// <value>
		///   <c>true</c> if this <see cref="RepositoryLocalObject" /> is shared; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsShared
		{
			get
			{
				return mBluePrintInfo.IsShared.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="RepositoryLocalObject" /> is localized.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="RepositoryLocalObject" /> is localized; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsLocalized
		{
			get
			{
				return mBluePrintInfo.IsLocalized.GetValueOrDefault(false);
			}
		}
	}
}
