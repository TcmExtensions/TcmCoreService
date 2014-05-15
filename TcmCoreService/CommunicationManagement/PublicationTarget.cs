#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Publication Target
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

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="PublicationTarget" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PublicationTargetData" />.
	/// </summary>
	public class PublicationTarget : PublishingTarget
	{
		private PublicationTargetData mPublicationTargetData;

		private ApprovalStatus mMinApprovalStatus = null;
		private IEnumerable<TargetDestination> mDestinations = null;
		private IEnumerable<Publication> mPublications = null;
		private IEnumerable<TargetType> mTargetTypes = null;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="PublicationTarget"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="publicationTargetData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublicationTargetData" /></param>
		protected PublicationTarget(Session session, PublicationTargetData publicationTargetData): base(session, publicationTargetData)
		{
			if (publicationTargetData == null)
				throw new ArgumentNullException("publicationTargetData");

			mPublicationTargetData = publicationTargetData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PublicationTarget"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal PublicationTarget(Session session, TcmUri uri): this(session, session.Read<PublicationTargetData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="PublicationTarget" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PublicationTargetData" />
		/// </summary>
		/// <param name="publicationTargetData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublicationTargetData" /></param>
		protected void Reload(PublicationTargetData publicationTargetData)
		{
			if (publicationTargetData == null)
				throw new ArgumentNullException("publicationTargetData");

			mPublicationTargetData = publicationTargetData;
			base.Reload(publicationTargetData);

			mMinApprovalStatus = null;
			mPublications = null;
			mTargetTypes = null;
		}

		/// <summary>
		/// Reload the <see cref="PublicationTarget" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<PublicationTargetData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget"/> default code page.
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget"/> default code page.
		/// </value>
		public int DefaultCodePage
		{
			get
			{
				return mPublicationTargetData.DefaultCodePage.GetValueOrDefault(0);
			}
			set
			{
				mPublicationTargetData.DefaultCodePage = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget"/> description.
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget"/> description.
		/// </value>
		public String Description
		{
			get
			{
				return mPublicationTargetData.Description;
			}
			set
			{
				mPublicationTargetData.Description = value;		
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget" /> <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.TargetDestination}" />
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget" /> <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.TargetDestination}" />
		/// </value>
		public IEnumerable<TargetDestination> TargetDestinations
		{
			get			
			{
				if (mDestinations == null && mPublicationTargetData.Destinations != null)
					mDestinations = mPublicationTargetData.Destinations.Select(destination => new TargetDestination(Session, destination));

				return mDestinations;
			}
			set
			{
				mDestinations = value;

				if (value != null)
				{
					mPublicationTargetData.Destinations = value.Select(destination => new TargetDestinationData()
					{
						ProtocolData = destination.ProtocolData,
						ProtocolSchema = new LinkToSchemaData()
						{
							IdRef = destination.ProtocolSchema.Id
						},
						Title = destination.Title
					}).ToArray();
				}
				else
					mPublicationTargetData.Destinations = null;
			}
		}

		/// <summary>
		/// Retrieves the minimum <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="PublicationTarget" />
		/// </summary>
		/// <value>
		/// Minimum <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="PublicationTarget" />
		/// </value>
		public ApprovalStatus MinApprovalStatus
		{
			get
			{
				if (mMinApprovalStatus == null && mPublicationTargetData.MinApprovalStatus.IdRef != TcmUri.NullUri)
					mMinApprovalStatus = new ApprovalStatus(Session, mPublicationTargetData.MinApprovalStatus.IdRef);

				return mMinApprovalStatus;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget" /> <see cref="T:Tridion.ContentManager.CoreService.Client.PublishPriority" />
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget" /> <see cref="T:Tridion.ContentManager.CoreService.Client.PublishPriority" />
		/// </value>
		public PublishPriority Priority
		{
			get
			{
				return mPublicationTargetData.Priority.GetValueOrDefault(PublishPriority.UnknownByClient);
			}
			set
			{
				mPublicationTargetData.Priority = value;
				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget" /> publications.
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget" /> publications.
		/// </value>
		public IEnumerable<Publication> Publications
		{
			get
			{
				if (mPublications == null && mPublicationTargetData.Publications != null)
					mPublications = mPublicationTargetData.Publications.Select(publication => new Publication(Session, publication.IdRef));

				return mPublications;
			}
			set
			{
				mPublications = value;

				if (value != null)
				{
					mPublicationTargetData.Publications = value.Select(publication => new LinkToPublicationData()
					{
						IdRef = publication.Id
					}).ToArray();
				}
				else
					mPublicationTargetData.Publications = null;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget" /> target language.
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget" /> target language.
		/// </value>
		public String TargetLanguage
		{
			get
			{
				return mPublicationTargetData.TargetLanguage;
			}
			set
			{
				mPublicationTargetData.TargetLanguage = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PublicationTarget" /> target types.
		/// </summary>
		/// <value>
		/// <see cref="PublicationTarget" /> target types.
		/// </value>
		public IEnumerable<TargetType> TargetTypes
		{
			get
			{
				if (mTargetTypes == null && mPublicationTargetData.TargetTypes != null)
					mTargetTypes = mPublicationTargetData.TargetTypes.Select(targetType => new TargetType(Session, targetType.IdRef));

				return mTargetTypes;
			}
			set
			{
				mTargetTypes = value;

				if (value != null)
				{
					mPublicationTargetData.TargetTypes = value.Select(targetType => new LinkToTargetTypeData()
					{
						IdRef = targetType.Id
					}).ToArray();
				}
				else
					mPublicationTargetData.TargetTypes = null;
			}
		}
	}
}
