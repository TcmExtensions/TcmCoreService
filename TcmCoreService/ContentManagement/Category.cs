#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Category
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
	/// <see cref="Category" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.CategoryData" />.
	/// </summary>
	public class Category : OrganizationalItem
	{
		private CategoryData mCategoryData;
		private IEnumerable<Category> mAllowedParentCategories = null;
		private Schema mKeywordMetadataSchema = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Category"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.CategoryData" /></param>
		protected Category(Client client, CategoryData categoryData): base(client, categoryData)
		{
			if (categoryData == null)
				throw new ArgumentNullException("categoryData");

			mCategoryData = categoryData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Category"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Category(Client client, TcmUri uri): this(client, client.Read<CategoryData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Category" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.CategoryData" />
		/// </summary>
		/// <param name="categoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.CategoryData" /></param>
		protected void Reload(CategoryData categoryData)
		{
			if (categoryData == null)
				throw new ArgumentNullException("categoryData");

			mCategoryData = categoryData;
			base.Reload(categoryData);

			mAllowedParentCategories = null;
			mKeywordMetadataSchema = null;
		}

		/// <summary>
		/// Reload the <see cref="Category" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<CategoryData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Category" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<CategoryData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Category" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<CategoryData>(this.Id));			
		}

		/// <summary>
		/// Retrieves the list of allowed parent <see cref="T:System.Collections.Generic.IEnumerable{Category}" /> for this <see cref="Category" />
		/// </summary>
		/// <value>
		/// List of allowed parent <see cref="T:System.Collections.Generic.IEnumerable{Category}" /> for this <see cref="Category" />
		/// </value>
		public IEnumerable<Category> AllowedParentCategories
		{
			get
			{
				if (mAllowedParentCategories == null && mCategoryData.AllowedParentCategories != null)
					mAllowedParentCategories = mCategoryData.AllowedParentCategories.Select(category => new Category(Client, category.IdRef));

				return mAllowedParentCategories;
			}
			set
			{
				mAllowedParentCategories = value;

				if (value != null)
				{
					mCategoryData.AllowedParentCategories = mAllowedParentCategories.Select(category => new LinkToCategoryData()
					{
						IdRef = category.Id
					}).ToArray();
				}
				else
					mCategoryData.AllowedParentCategories = null;
			}
		}

		/// <summary>
		/// Gets the <see cref="Category" /> description
		/// </summary>
		/// <value>
		/// <see cref="Category" /> description
		/// </value>
		public String Description
		{
			get
			{
				return mCategoryData.Description;
			}
			set
			{
				if (!String.IsNullOrEmpty(Description))
					mCategoryData.Description = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Category" /> keyword metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Category" /> keyword metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema KeywordMetadataSchema
		{
			get
			{
				if (mKeywordMetadataSchema == null && mCategoryData.KeywordMetadataSchema.IdRef != TcmUri.NullUri)
					mKeywordMetadataSchema = new Schema(Client, mCategoryData.KeywordMetadataSchema.IdRef);

				return mKeywordMetadataSchema;
			}
			set
			{
				if (value != null)
					mCategoryData.KeywordMetadataSchema.IdRef = value.Id;
				else
					mCategoryData.KeywordMetadataSchema.IdRef = TcmUri.NullUri;

				mKeywordMetadataSchema = null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Category" /> is a taxonomy root.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Category" /> is a taxonomy root; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsTaxonomyRoot
		{
			get
			{
				return mCategoryData.IsTaxonomyRoot.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use this <see cref="Category"/> for identification.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Category" /> is used for identification; otherwise, <c>false</c>.
		/// </value>
		public Boolean UseForIdentification
		{
			get
			{
				return mCategoryData.UseForIdentification.GetValueOrDefault(false);
			}
			set
			{
				mCategoryData.UseForIdentification = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use this <see cref="Category"/> for navigation.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Category" /> is used for navigation; otherwise, <c>false</c>.
		/// </value>
		public Boolean UseForNavigation
		{
			get
			{
				return mCategoryData.UseForNavigation.GetValueOrDefault(false);
			}
			set
			{
				mCategoryData.UseForNavigation = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="Category" /> XmlName
		/// </summary>
		/// <value>
		/// <see cref="Category" /> XmlName
		/// </value>
		public String XmlName
		{
			get
			{
				return mCategoryData.XmlName;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mCategoryData.XmlName = value;
			}
		}
	}
}
