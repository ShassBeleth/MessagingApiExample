using MessagingApiTemplate.Models.Requests.SendMessage;
using System;
using MessagingApiTemplate.Models.Requests.SendMessage.ImageMap;
using MessagingApiTemplate.Models.Requests.SendMessage.Template;
using MessagingApiTemplate.Utils;

namespace MessagingApiTemplate.Services.Message.Factory {

	/// <summary>
	/// メッセージ作成Service
	/// </summary>
	public class MessageFactoryService {

		/// <summary>
		/// メッセージ
		/// </summary>
		internal MessageBase[] Messages { private set; get; }

		/// <summary>
		/// コンストラクタは隠す
		/// </summary>
		private MessageFactoryService() { }

		/// <summary>
		/// メッセージ作成
		/// </summary>
		public static MessageFactoryService CreateMessage() {
			Trace.TraceInformation( "Start" );
			MessageFactoryService messageFactoryService = new MessageFactoryService();
			Trace.TraceInformation( "End" );
			return messageFactoryService;
		}

		/// <summary>
		/// 配列の数を追加する
		/// </summary>
		/// <returns>配列が既に5つたまっていた場合は何もしない</returns>
		private bool RegulateMessageArray() {

			Trace.TraceInformation( "Start" );

			if( this.Messages == null ) {
				Trace.TraceInformation( "Messages is Null" );
				this.Messages = new MessageBase[ 1 ];
				Trace.TraceInformation( "End" );
				return true;
			}
			else {
				if( this.Messages.Length == 5 ) {
					Trace.TraceWarning( "Messages Length is Max" );
					Trace.TraceInformation( "End" );
					return false;
				}
				else {
					Trace.TraceInformation( "Messages Length is not Max" );
					MessageBase[] messages = this.Messages;
					Array.Resize(
						ref messages ,
						this.Messages.Length + 1
					);
					this.Messages = messages;
					Trace.TraceInformation( "End" );
					return true;
				}
			}

		}

		/// <summary>
		/// テキストメッセージ追加
		/// </summary>
		/// <param name="text">テキスト</param>
		public MessageFactoryService AddTextMessage( string text ) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			TextMessage textMessage = new TextMessage() {
				text = text
			};
			this.Messages[ this.Messages.Length - 1 ] = textMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// スタンプメッセージ追加
		/// </summary>
		/// <param name="packageId">パッケージID</param>
		/// <param name="stickerId">スタンプID</param>
		public MessageFactoryService AddStickerMessage( string packageId , string stickerId ) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			StickerMessage stickerMessage = new StickerMessage() {
				packageId = packageId ,
				stickerId = stickerId
			};
			this.Messages[ this.Messages.Length - 1 ] = stickerMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// 画像メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">画像URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public MessageFactoryService AddImageMessage( string originalContentUrl , string previewImageUrl ) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			ImageMessage imageMessage = new ImageMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.Messages[ this.Messages.Length - 1 ] = imageMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// 動画メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">動画URL</param>
		/// <param name="previewImageUrl">プレビュー画像URL</param>
		public MessageFactoryService AddVideoMessage( string originalContentUrl , string previewImageUrl ) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			VideoMessage videoMessage = new VideoMessage() {
				originalContentUrl = originalContentUrl ,
				previewImageUrl = previewImageUrl
			};
			this.Messages[ this.Messages.Length - 1 ] = videoMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// 音声メッセージ追加
		/// </summary>
		/// <param name="originalContentUrl">音声URL</param>
		/// <param name="duration">音声ファイルの長さ</param>
		public MessageFactoryService AddAudioMessage( string originalContentUrl , int duration ) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			AudioMessage audioMessage = new AudioMessage() {
				originalContentUrl = originalContentUrl ,
				duration = duration
			};
			this.Messages[ this.Messages.Length - 1 ] = audioMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// 位置情報メッセージ追加
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="address">住所</param>
		/// <param name="latitude">緯度</param>
		/// <param name="longitude">経度</param>
		public MessageFactoryService AddLocationMessage(
			string title ,
			string address ,
			decimal latitude ,
			decimal longitude
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			LocationMessage locationMessage = new LocationMessage() {
				title = title ,
				address = address ,
				latitude = latitude ,
				longitude = longitude
			};
			this.Messages[ this.Messages.Length - 1 ] = locationMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// イメージマップメッセージ追加
		/// </summary>
		/// <param name="baseUrl">画像URL</param>
		/// <param name="altText">代替テキスト</param>
		/// <param name="width">画像幅</param>
		/// <param name="height">画像幅を1040とした時の高さ</param>
		/// <param name="imageMapActionFfactoryService">画像がタップされた時のアクション</param>
		/// <returns></returns>
		public MessageFactoryService AddImageMapMessage(
			string baseUrl ,
			string altText ,
			ImageMapActionFactoryService imageMapActionFfactoryService ,
			int height ,
			int width = 1040
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			ImageMapMessage imageMapMessage = new ImageMapMessage() {
				baseUrl = baseUrl ,
				altText = altText ,
				baseSize = new BaseSize() {
					width = width ,
					height = height
				} ,
				actions = imageMapActionFfactoryService.Actions
			};
			this.Messages[ this.Messages.Length - 1 ] = imageMapMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// ボタンテンプレート追加
		/// </summary>
		/// <param name="altText">代替テキスト</param>
		/// <param name="thumbnailImageUrl">サムネ画像URL</param>
		/// <param name="imageBackgroundColor">画像背景色</param>
		/// <param name="title">タイトル</param>
		/// <param name="text">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		/// <param name="isImageAspectSquare">アスペクト比を1:1にするかどうか</param>
		/// <param name="isImageSizeCover">余白を付けずに表示するかどうか</param>
		public MessageFactoryService AddButtonTemplateMessage(
			string altText , 
			string thumbnailImageUrl ,
			string imageBackgroundColor ,
			string title ,
			string text ,
			TemplateActionFactoryService templateActionFactoryService ,
			bool isImageAspectSquare = false ,
			bool isImageSizeCover = true
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			MessageBase templateMessage = new TemplateMessage() {
				altText = altText ,
				template = new ButtonTemplate() {
					thumbnailImageUrl = thumbnailImageUrl ,
					imageAspectRatio = isImageAspectSquare ? "square" : "rectangle" ,
					imageSize = isImageSizeCover ? "cover" : "contain" ,
					imageBackgroundColor = imageBackgroundColor ,
					title = title ,
					text = text ,
					actions = templateActionFactoryService.Actions
				}
			};
			this.Messages[ this.Messages.Length - 1 ] = templateMessage;

			Trace.TraceInformation( "End" );

			return this;
		}

		/// <summary>
		/// 確認テンプレート追加
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public MessageFactoryService AddConfirmTemplateMessage(
			string text ,
			TemplateActionFactoryService templateActionFactoryService
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			MessageBase templateMessage = new TemplateMessage() {
				altText = text ,
				template = new ConfirmTemplate() {
					text = text ,
					actions = templateActionFactoryService.Actions
				}
			};
			this.Messages[ this.Messages.Length - 1 ] = templateMessage;

			Trace.TraceInformation( "End" );

			return this;
		}

		/// <summary>
		/// カルーセルテンプレート追加
		/// </summary>
		/// <param name="altText">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public MessageFactoryService AddCarouselTemplateMessage(
			TemplateCarouselColumnFactoryService templateCarouselColumnFactoryService ,
			string altText ,
			bool isImageAspectSquare = false ,
			bool isImageSizeCover = true
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			MessageBase templateMessage = new TemplateMessage() {
				altText = altText ,
				template = new CarouselTemplate() {
					columns = templateCarouselColumnFactoryService.Columns ,
					imageAspectRatio = isImageAspectSquare ? "square" : "rectangle" ,
					imageSize = isImageSizeCover ? "cover" : "contain"
				}
			};
			this.Messages[ this.Messages.Length - 1 ] = templateMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

		/// <summary>
		/// 画像カルーセルテンプレート追加
		/// </summary>
		/// <param name="altText">テキスト</param>
		/// <param name="templateActionFactoryService">アクション作成用クラス</param>
		public MessageFactoryService AddImageCarouselTemplateMessage(
			TemplateImageCarouselColumnFactoryService templateimageCarouselColumnFactoryService ,
			string altText
		) {

			Trace.TraceInformation( "Start" );

			if( !this.RegulateMessageArray() ) {
				Trace.TraceWarning( "Regulate Message Array is False" );
				Trace.TraceInformation( "End" );
				return this;
			}

			MessageBase templateMessage = new TemplateMessage() {
				altText = altText ,
				template = new ImageCarouselTemplate() {					
					columns = templateimageCarouselColumnFactoryService.Columns ,
				}
			};
			this.Messages[ this.Messages.Length - 1 ] = templateMessage;

			Trace.TraceInformation( "End" );

			return this;

		}

	}

}