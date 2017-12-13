using System;
using System.Diagnostics;
using MessagingApiTemplate.Models.Requests.SendMessage.Template.ImageCarousel;

namespace MessagingApiTemplate.Services {

	/// <summary>
	/// 画像カルーセルのカラム作成用クラス
	/// </summary>
	public class TemplateImageCarouselColumnFactoryService {

		/// <summary>
		/// カラム
		/// </summary>
		internal ImageCarouselColumn[] Columns { private set; get; }

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private TemplateImageCarouselColumnFactoryService() { }

		/// <summary>
		/// カラム作成
		/// </summary>
		public static TemplateImageCarouselColumnFactoryService CreateColumn() {
			TemplateImageCarouselColumnFactoryService templateImageCarouselColumnFactoryService = new TemplateImageCarouselColumnFactoryService();
			return templateImageCarouselColumnFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateColumnArray() {

			Trace.TraceInformation( "Start Regulate Column Array" );

			if( this.Columns == null ) {
				Trace.TraceInformation( "Column is Null" );
				this.Columns = new ImageCarouselColumn[ 1 ];
				return true;
			}
			else {
				if( this.Columns.Length == 5 ) {
					Trace.TraceWarning( "Column Length is Max" );
					return false;
				}
				else {
					Trace.TraceInformation( "Column Length is not Max" );
					ImageCarouselColumn[] column = this.Columns;
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
		public TemplateImageCarouselColumnFactoryService AddColumn(
			string imageUri ,
			TemplateActionFactoryService templateActionFactoryService
		) {

			Trace.TraceInformation( "Start Add Column" );

			if( !this.RegulateColumnArray() ) {
				Trace.TraceWarning( "Regulate Column Array is False" );
				return this;
			}
			
			ImageCarouselColumn column = new ImageCarouselColumn() {
				imageUrl = imageUri ,
				action = templateActionFactoryService.Actions[0]
			};
			this.Columns[ this.Columns.Length - 1 ] = column;
			
			return this;

		}

	}

}