using System;
using System.Diagnostics;
using MessagingApiTemplate.Models.Requests.SendMessage.Template.Carousel;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// カルーセルのカラム作成用クラス
	/// </summary>
	public class TemplateCarouselColumnFactoryService {

		/// <summary>
		/// カラム
		/// </summary>
		internal CarouselColumn[] Columns { private set; get; }

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private TemplateCarouselColumnFactoryService() { }

		/// <summary>
		/// カラム作成
		/// </summary>
		public static TemplateCarouselColumnFactoryService CreateMessage() {
			TemplateCarouselColumnFactoryService templateCarouselColumnFactoryService = new TemplateCarouselColumnFactoryService();
			return templateCarouselColumnFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateColumnArray() {

			Trace.TraceInformation( "Start Regulate Column Array" );

			if( this.Columns == null ) {
				Trace.TraceInformation( "Column is Null" );
				this.Columns = new CarouselColumn[ 1 ];
				return true;
			}
			else {
				if( this.Columns.Length == 5 ) {
					Trace.TraceWarning( "Column Length is Max" );
					return false;
				}
				else {
					Trace.TraceInformation( "Column Length is not Max" );
					CarouselColumn[] column = this.Columns;
					Array.Resize(
						ref column ,
						this.Columns.Length + 1
					);
					this.Columns = column;
					return true;
				}
			}

		}

		/// <summary>
		/// カラム追加
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="text">テキスト</param>
		/// <param name="thumbnailImageUrl">サムネ画像URL</param>
		/// <param name="imageBackgroundColor">画像背景色</param>
		/// <param name="templateActionFactoryService">アクション作成クラス</param>
		/// <returns></returns>
		public TemplateCarouselColumnFactoryService AddColumn(
			string title , 
			string text ,
			string thumbnailImageUrl ,
			string imageBackgroundColor ,
			TemplateActionFactoryService templateActionFactoryService
		) {

			Trace.TraceInformation( "Start Add Column" );

			if( !this.RegulateColumnArray() ) {
				Trace.TraceWarning( "Regulate Column Array is False" );
				return this;
			}

			CarouselColumn column = new CarouselColumn() {
				title = title ,
				text = text ,
				thumbnailImageUrl = thumbnailImageUrl ,
				imageBackgroundColor = imageBackgroundColor ,
				actions = templateActionFactoryService.Actions
			};
			this.Columns[ this.Columns.Length - 1 ] = column;


			return this;

		}


	}

}