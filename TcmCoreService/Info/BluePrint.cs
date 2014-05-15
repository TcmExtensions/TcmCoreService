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
using TcmCoreService.ContentManagement;
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

		/// <summary>
		/// Initializes a new instance of the <see cref="BluePrint"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="bluePrintInfo"><see cref="T:Tridion.ContentManager.CoreService.Client.BluePrintInfo" /></param>
		public BluePrint(Session session, BluePrintInfo bluePrintInfo): base(session)
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
					mOwningRepository = new Repository(Session, mBluePrintInfo.OwningRepository.IdRef);

				return mOwningRepository;
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
