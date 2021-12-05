namespace MeasurementsModule.Panels.KWP
{
    sealed partial class KwpMeasurements
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KwpMeasurements));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this._measurementTreeList = new De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this._backBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._loadConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._channelNrBarEditItem = new DevExpress.XtraBars.BarEditItem();
            this._channelNrEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._addChannelBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._removeChannelsBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._saveConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._removeConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._nextBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.mainViewRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this._navigationRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._moduleFunctionsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._configurationRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._channelNrEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this._measurementTreeList);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1022, 423, 602, 458);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // _measurementTreeList
            // 
            resources.ApplyResources(this._measurementTreeList, "_measurementTreeList");
            this._measurementTreeList.Name = "_measurementTreeList";
            this._measurementTreeList.OnButtonStateChanged = null;
            this._measurementTreeList.OnMeasurementTreeListKeyDown = null;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1051, 526);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this._measurementTreeList;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1031, 506);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this._backBarButtonItem,
            this._loadConfigBarButtonItem,
            this._channelNrBarEditItem,
            this._addChannelBarButtonItem,
            this._removeChannelsBarButtonItem,
            this._saveConfigBarButtonItem,
            this._removeConfigBarButtonItem,
            this._nextBarButtonItem});
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.mainViewRibbonPage});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._channelNrEdit});
            // 
            // _backBarButtonItem
            // 
            resources.ApplyResources(this._backBarButtonItem, "_backBarButtonItem");
            this._backBarButtonItem.Id = 1;
            this._backBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_backBarButtonItem.ImageOptions.SvgImage")));
            this._backBarButtonItem.Name = "_backBarButtonItem";
            this._backBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnSwitchBackClick);
            // 
            // _loadConfigBarButtonItem
            // 
            resources.ApplyResources(this._loadConfigBarButtonItem, "_loadConfigBarButtonItem");
            this._loadConfigBarButtonItem.Id = 2;
            this._loadConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_loadConfigBarButtonItem.ImageOptions.SvgImage")));
            this._loadConfigBarButtonItem.Name = "_loadConfigBarButtonItem";
            this._loadConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.LoadConfigurationClick);
            // 
            // _channelNrBarEditItem
            // 
            resources.ApplyResources(this._channelNrBarEditItem, "_channelNrBarEditItem");
            this._channelNrBarEditItem.Edit = this._channelNrEdit;
            this._channelNrBarEditItem.Id = 3;
            this._channelNrBarEditItem.Name = "_channelNrBarEditItem";
            // 
            // _channelNrEdit
            // 
            resources.ApplyResources(this._channelNrEdit, "_channelNrEdit");
            this._channelNrEdit.Name = "_channelNrEdit";
            _channelNrEdit.EditValueChanging += OnChannelNrEdit_EditValueChanging;
            // 
            // _addChannelBarButtonItem
            // 
            resources.ApplyResources(this._addChannelBarButtonItem, "_addChannelBarButtonItem");
            this._addChannelBarButtonItem.Enabled = false;
            this._addChannelBarButtonItem.Id = 4;
            this._addChannelBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_addChannelBarButtonItem.ImageOptions.SvgImage")));
            this._addChannelBarButtonItem.Name = "_addChannelBarButtonItem";
            this._addChannelBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnAddChannelClick);
            // 
            // _removeChannelsBarButtonItem
            // 
            resources.ApplyResources(this._removeChannelsBarButtonItem, "_removeChannelsBarButtonItem");
            this._removeChannelsBarButtonItem.Id = 5;
            this._removeChannelsBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_removeChannelsBarButtonItem.ImageOptions.SvgImage")));
            this._removeChannelsBarButtonItem.Name = "_removeChannelsBarButtonItem";
            this._removeChannelsBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnDeleteChannelClick);
            // 
            // _saveConfigBarButtonItem
            // 
            resources.ApplyResources(this._saveConfigBarButtonItem, "_saveConfigBarButtonItem");
            this._saveConfigBarButtonItem.Id = 6;
            this._saveConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_saveConfigBarButtonItem.ImageOptions.SvgImage")));
            this._saveConfigBarButtonItem.Name = "_saveConfigBarButtonItem";
            this._saveConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveConfigurationClick);
            // 
            // _removeConfigBarButtonItem
            // 
            resources.ApplyResources(this._removeConfigBarButtonItem, "_removeConfigBarButtonItem");
            this._removeConfigBarButtonItem.Id = 7;
            this._removeConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_removeConfigBarButtonItem.ImageOptions.SvgImage")));
            this._removeConfigBarButtonItem.Name = "_removeConfigBarButtonItem";
            this._removeConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RemoveConfigurationClick);
            // 
            // _nextBarButtonItem
            // 
            resources.ApplyResources(this._nextBarButtonItem, "_nextBarButtonItem");
            this._nextBarButtonItem.Id = 8;
            this._nextBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_nextBarButtonItem.ImageOptions.SvgImage")));
            this._nextBarButtonItem.Name = "_nextBarButtonItem";
            this._nextBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnNextClick);
            // 
            // mainViewRibbonPage
            // 
            this.mainViewRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this._navigationRibbonPageGroup,
            this._moduleFunctionsRibbonPageGroup,
            this._configurationRibbonPageGroup});
            this.mainViewRibbonPage.Name = "mainViewRibbonPage";
            resources.ApplyResources(this.mainViewRibbonPage, "mainViewRibbonPage");
            // 
            // _navigationRibbonPageGroup
            // 
            this._navigationRibbonPageGroup.AllowTextClipping = false;
            this._navigationRibbonPageGroup.ItemLinks.Add(this._backBarButtonItem);
            this._navigationRibbonPageGroup.ItemLinks.Add(this._nextBarButtonItem);
            this._navigationRibbonPageGroup.Name = "_navigationRibbonPageGroup";
            resources.ApplyResources(this._navigationRibbonPageGroup, "_navigationRibbonPageGroup");
            // 
            // _moduleFunctionsRibbonPageGroup
            // 
            this._moduleFunctionsRibbonPageGroup.AllowTextClipping = false;
            this._moduleFunctionsRibbonPageGroup.ItemLinks.Add(this._channelNrBarEditItem);
            this._moduleFunctionsRibbonPageGroup.ItemLinks.Add(this._addChannelBarButtonItem);
            this._moduleFunctionsRibbonPageGroup.ItemLinks.Add(this._removeChannelsBarButtonItem);
            this._moduleFunctionsRibbonPageGroup.Name = "_moduleFunctionsRibbonPageGroup";
            resources.ApplyResources(this._moduleFunctionsRibbonPageGroup, "_moduleFunctionsRibbonPageGroup");
            // 
            // _configurationRibbonPageGroup
            // 
            this._configurationRibbonPageGroup.AllowTextClipping = false;
            this._configurationRibbonPageGroup.ItemLinks.Add(this._loadConfigBarButtonItem);
            this._configurationRibbonPageGroup.ItemLinks.Add(this._saveConfigBarButtonItem);
            this._configurationRibbonPageGroup.ItemLinks.Add(this._removeConfigBarButtonItem);
            this._configurationRibbonPageGroup.Name = "_configurationRibbonPageGroup";
            resources.ApplyResources(this._configurationRibbonPageGroup, "_configurationRibbonPageGroup");
            // 
            // KwpMeasurements
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "KwpMeasurements";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._channelNrEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList _measurementTreeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _navigationRibbonPageGroup;
        internal DevExpress.XtraBars.Ribbon.RibbonPage mainViewRibbonPage;
        private DevExpress.XtraBars.BarButtonItem _backBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _loadConfigBarButtonItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _configurationRibbonPageGroup;
        private DevExpress.XtraBars.BarEditItem _channelNrBarEditItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit _channelNrEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _moduleFunctionsRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem _addChannelBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _removeChannelsBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _saveConfigBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _removeConfigBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _nextBarButtonItem;
    }
}
