namespace ALauncher
{
	partial class MainComponent
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainComponent));
			this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this._contextMenuStripNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._toolStripMenuItemReloadSettingsXML = new System.Windows.Forms.ToolStripMenuItem();
			this._toolStripMenuItemTestCode = new System.Windows.Forms.ToolStripMenuItem();
			this._toolStripMenuItemVersionInfo = new System.Windows.Forms.ToolStripMenuItem();
			this._toolStripMenuItemEndProgram = new System.Windows.Forms.ToolStripMenuItem();
			this._contextMenuStripNotify.SuspendLayout();
			// 
			// _notifyIcon
			// 
			this._notifyIcon.ContextMenuStrip = this._contextMenuStripNotify;
			this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
			this._notifyIcon.Text = "ALauncher";
			this._notifyIcon.Visible = true;
			// 
			// _contextMenuStripNotify
			// 
			this._contextMenuStripNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItemReloadSettingsXML,
            this._toolStripMenuItemTestCode,
            this._toolStripMenuItemVersionInfo,
            this._toolStripMenuItemEndProgram});
			this._contextMenuStripNotify.Name = "_contextMenuStripNotify";
			this._contextMenuStripNotify.Size = new System.Drawing.Size(182, 92);
			// 
			// _toolStripMenuItemSettings
			// 
			this._toolStripMenuItemReloadSettingsXML.Name = "_toolStripMenuItemSettings";
			this._toolStripMenuItemReloadSettingsXML.Size = new System.Drawing.Size(181, 22);
			this._toolStripMenuItemReloadSettingsXML.Text = "設定XMLの再読み込み";
			// 
			// _toolStripMenuItemTestCode
			// 
			this._toolStripMenuItemTestCode.Name = "_toolStripMenuItemTestCode";
			this._toolStripMenuItemTestCode.Size = new System.Drawing.Size(181, 22);
			this._toolStripMenuItemTestCode.Text = "Test Code(Debug)";
			// 
			// _toolStripMenuItemVersionInfo
			// 
			this._toolStripMenuItemVersionInfo.Name = "_toolStripMenuItemVersionInfo";
			this._toolStripMenuItemVersionInfo.Size = new System.Drawing.Size(181, 22);
			this._toolStripMenuItemVersionInfo.Text = "バージョン情報";
			// 
			// _toolStripMenuItemEndProgram
			// 
			this._toolStripMenuItemEndProgram.Name = "_toolStripMenuItemEndProgram";
			this._toolStripMenuItemEndProgram.Size = new System.Drawing.Size(181, 22);
			this._toolStripMenuItemEndProgram.Text = "プログラムの終了";
			this._contextMenuStripNotify.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon _notifyIcon;
		private System.Windows.Forms.ContextMenuStrip _contextMenuStripNotify;
		private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemReloadSettingsXML;
		private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemTestCode;
		private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemVersionInfo;
		private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItemEndProgram;
	}
}
