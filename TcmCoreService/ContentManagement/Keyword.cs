#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Keyword
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
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="Keyword" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.KeywordData" />.
	/// </summary>
	public class Keyword : RepositoryLocalObject
	{
		private KeywordData mKeywordData;
		private IEnumerable<Keyword> mParentKeywords = null;
		private IEnumerable<Keyword> mRelatedKeywords = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Keyword"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="keywordData"><see cref="T:Tridion.ContentManager.CoreService.Client.KeywordData" /></param>
		protected Keyword(Client client, KeywordData keywordData): base(client, keywordData)
		{
			if (keywordData == null)
				throw new ArgumentNullException("keywordData");

			mKeywordData = keywordData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Keyword"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Keyword(Client client, TcmUri uri): this(client, client.Read<KeywordData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Keyword" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.KeywordData" />
		/// </summary>
		/// <param name="keywordData"><see cref="T:Tridion.ContentManager.CoreService.Client.KeywordData" /></param>
		protected void Reload(KeywordData keywordData)
		{
			if (keywordData == null)
				throw new ArgumentNullException("keywordData");

			mKeywordData = keywordData;
			base.Reload(keywordData);

			mParentKeywords = null;
			mRelatedKeywords = null;
		}

		/// <summary>
		/// Reload the <see cref="Keyword" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<KeywordData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Keyword" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<KeywordData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Keyword" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<KeywordData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Keyword" /> is abstract.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Keyword" /> is abstract; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsAbstract
		{
			get
			{
				return mKeywordData.IsAbstract.GetValueOrDefault(false);
			}
			set
			{
				mKeywordData.IsAbstract = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this is a root <see cref="Keyword" />.
		/// </summary>
		/// <value>
		/// <c>true</c> if this is a root <see cref="Keyword" />; otherwise, <c>false</c>.
		/// </value>				
		public Boolean IsRoot
		{
			get
			{
				return mKeywordData.IsRoot.GetValueOrDefault(false);
			}
			set
			{
				mKeywordData.IsAbstract = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Keyword" /> key.
		/// </summary>
		/// <value>
		/// <see cref="Keyword" /> key.
		/// </value>
		public String Key
		{
			get
			{
				return mKeywordData.Key;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mKeywordData.Key = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Keyword" /> description.
		/// </summary>
		/// <value>
		/// <see cref="Keyword" /> description.
		/// </value>
		public String Description
		{
			get
			{
				return mKeywordData.Description;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mKeywordData.Description = value;
			}
		}

		/// <summary>
		/// Retrieves the list of parent <see cref="T:System.Collections.Generic.IEnumerable{Keyword}" /> for this <see cref="Keyword" />
		/// </summary>
		/// <value>
		/// List of parent <see cref="T:System.Collections.Generic.IEnumerable{Keyword}" /> for this <see cref="Keyword" />
		/// </value>
		public IEnumerable<Keyword> ParentKeywords
		{
			get
			{
				if (mParentKeywords == null && mKeywordData.ParentKeywords != null)
					mParentKeywords = mKeywordData.ParentKeywords.Select(keyword => new Keyword(Client, keyword.IdRef));

				return mParentKeywords;
			}
			set
			{
				mParentKeywords = value;

				if (value != null)
					mKeywordData.ParentKeywords = value.Select(keyword => new LinkToKeywordData()
					{
						IdRef = keyword.Id
					}).ToArray();
				else
					mKeywordData.ParentKeywords = null;
			}
		}

		/// <summary>
		/// Retrieves the list of related <see cref="T:System.Collections.Generic.IEnumerable{Keyword}" /> for this <see cref="Keyword" />
		/// </summary>
		/// <value>
		/// List of parent <see cref="T:System.Collections.Generic.IEnumerable{Keyword}" /> for this <see cref="Keyword" />
		/// </value>
		public IEnumerable<Keyword> RelatedKeywords
		{
			get
			{
				if (mRelatedKeywords == null)
					mRelatedKeywords = mKeywordData.RelatedKeywords.Select(keyword => new Keyword(Client, keyword.IdRef));

				return mRelatedKeywords;
			}
			set
			{
				if (value != null)
					mKeywordData.RelatedKeywords = value.Select(keyword => new LinkToKeywordData()
					{
						IdRef = keyword.Id
					}).ToArray();
				else
					mKeywordData.RelatedKeywords = null;
			}
		}
	}
}
