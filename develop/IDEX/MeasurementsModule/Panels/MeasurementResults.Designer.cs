namespace MeasurementsModule.Panels
{
    internal sealed partial class MeasurementResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasurementResults));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this._measurementTreeList = new De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.MeasurementControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this._backBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._startBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._openFileLocationBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._startCsvRecordBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._stopCsvRecordBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._progressBarEditItem = new DevExpress.XtraBars.BarEditItem();
            this._repositoryItemMarqueeProgressBar = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this._printProtocolBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.mainViewRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this._naivgationRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._moduleFunctionsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._recordRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._protocolRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._progressRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._startButtonSvgImageCollection = new DevExpress.Utils.SvgImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeasurementControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryItemMarqueeProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._startButtonSvgImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this._measurementTreeList);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-874, 514, 684, 350);
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
            this.MeasurementControlItem});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1110, 563);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // MeasurementControlItem
            // 
            resources.ApplyResources(this.MeasurementControlItem, "MeasurementControlItem");
            this.MeasurementControlItem.Control = this._measurementTreeList;
            this.MeasurementControlItem.Location = new System.Drawing.Point(0, 0);
            this.MeasurementControlItem.Name = "MeasurementControlItem";
            this.MeasurementControlItem.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.MeasurementControlItem.Size = new System.Drawing.Size(1090, 543);
            this.MeasurementControlItem.TextSize = new System.Drawing.Size(0, 0);
            this.MeasurementControlItem.TextVisible = false;
            // 
            // ribbonControl1
            // 
            resources.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.ImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.LargeImageIndex")));
            this.ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonControl1.ExpandCollapseItem.ImageOptions.SvgImage")));
            this.ribbonControl1.ExpandCollapseItem.SearchTags = resources.GetString("ribbonControl1.ExpandCollapseItem.SearchTags");
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this._backBarButtonItem,
            this._startBarButtonItem,
            this._openFileLocationBarButtonItem,
            this._startCsvRecordBarButtonItem,
            this._stopCsvRecordBarButtonItem,
            this._progressBarEditItem,
            this._printProtocolBarButtonItem});
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.mainViewRibbonPage});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._repositoryItemMarqueeProgressBar});
            // 
            // _backBarButtonItem
            // 
            resources.ApplyResources(this._backBarButtonItem, "_backBarButtonItem");
            this._backBarButtonItem.Id = 1;
            this._backBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_backBarButtonItem.ImageOptions.ImageIndex")));
            this._backBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_backBarButtonItem.ImageOptions.LargeImageIndex")));
            this._backBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_backBarButtonItem.ImageOptions.SvgImage")));
            this._backBarButtonItem.Name = "_backBarButtonItem";
            this._backBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnSwitchBackClick);
            // 
            // _startBarButtonItem
            // 
            resources.ApplyResources(this._startBarButtonItem, "_startBarButtonItem");
            this._startBarButtonItem.Id = 2;
            this._startBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_startBarButtonItem.ImageOptions.ImageIndex")));
            this._startBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_startBarButtonItem.ImageOptions.LargeImageIndex")));
            this._startBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_startBarButtonItem.ImageOptions.SvgImage")));
            this._startBarButtonItem.Name = "_startBarButtonItem";
            this._startBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnStartClick);
            // 
            // _openFileLocationBarButtonItem
            // 
            resources.ApplyResources(this._openFileLocationBarButtonItem, "_openFileLocationBarButtonItem");
            this._openFileLocationBarButtonItem.Id = 3;
            this._openFileLocationBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_openFileLocationBarButtonItem.ImageOptions.ImageIndex")));
            this._openFileLocationBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_openFileLocationBarButtonItem.ImageOptions.LargeImageIndex")));
            this._openFileLocationBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_openFileLocationBarButtonItem.ImageOptions.SvgImage")));
            this._openFileLocationBarButtonItem.Name = "_openFileLocationBarButtonItem";
            this._openFileLocationBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenFileLocationClicked);
            // 
            // _startCsvRecordBarButtonItem
            // 
            resources.ApplyResources(this._startCsvRecordBarButtonItem, "_startCsvRecordBarButtonItem");
            this._startCsvRecordBarButtonItem.Id = 4;
            this._startCsvRecordBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_startCsvRecordBarButtonItem.ImageOptions.ImageIndex")));
            this._startCsvRecordBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_startCsvRecordBarButtonItem.ImageOptions.LargeImageIndex")));
            this._startCsvRecordBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_startCsvRecordBarButtonItem.ImageOptions.SvgImage")));
            this._startCsvRecordBarButtonItem.Name = "_startCsvRecordBarButtonItem";
            this._startCsvRecordBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.StartCsvLoggingBtnClicked);
            // 
            // _stopCsvRecordBarButtonItem
            // 
            resources.ApplyResources(this._stopCsvRecordBarButtonItem, "_stopCsvRecordBarButtonItem");
            this._stopCsvRecordBarButtonItem.Id = 5;
            this._stopCsvRecordBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_stopCsvRecordBarButtonItem.ImageOptions.ImageIndex")));
            this._stopCsvRecordBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_stopCsvRecordBarButtonItem.ImageOptions.LargeImageIndex")));
            this._stopCsvRecordBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_stopCsvRecordBarButtonItem.ImageOptions.SvgImage")));
            this._stopCsvRecordBarButtonItem.Name = "_stopCsvRecordBarButtonItem";
            this._stopCsvRecordBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.StopCsvLoggingBtnClicked);
            // 
            // _progressBarEditItem
            // 
            resources.ApplyResources(this._progressBarEditItem, "_progressBarEditItem");
            this._progressBarEditItem.Edit = this._repositoryItemMarqueeProgressBar;
            this._progressBarEditItem.Id = 6;
            this._progressBarEditItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_progressBarEditItem.ImageOptions.ImageIndex")));
            this._progressBarEditItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_progressBarEditItem.ImageOptions.LargeImageIndex")));
            this._progressBarEditItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_progressBarEditItem.ImageOptions.SvgImage")));
            this._progressBarEditItem.Name = "_progressBarEditItem";
            // 
            // _repositoryItemMarqueeProgressBar
            // 
            resources.ApplyResources(this._repositoryItemMarqueeProgressBar, "_repositoryItemMarqueeProgressBar");
            this._repositoryItemMarqueeProgressBar.Name = "_repositoryItemMarqueeProgressBar";
            this._repositoryItemMarqueeProgressBar.ShowTitle = true;
            // 
            // _printProtocolBarButtonItem
            // 
            resources.ApplyResources(this._printProtocolBarButtonItem, "_printProtocolBarButtonItem");
            this._printProtocolBarButtonItem.Id = 7;
            this._printProtocolBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_printProtocolBarButtonItem.ImageOptions.ImageIndex")));
            this._printProtocolBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_printProtocolBarButtonItem.ImageOptions.LargeImageIndex")));
            this._printProtocolBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_printProtocolBarButtonItem.ImageOptions.SvgImage")));
            this._printProtocolBarButtonItem.Name = "_printProtocolBarButtonItem";
            this._printProtocolBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnPrintProtocolClick);
            // 
            // mainViewRibbonPage
            // 
            this.mainViewRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this._naivgationRibbonPageGroup,
            this._moduleFunctionsRibbonPageGroup,
            this._recordRibbonPageGroup,
            this._protocolRibbonPageGroup,
            this._progressRibbonPageGroup});
            this.mainViewRibbonPage.Name = "mainViewRibbonPage";
            resources.ApplyResources(this.mainViewRibbonPage, "mainViewRibbonPage");
            // 
            // _naivgationRibbonPageGroup
            // 
            this._naivgationRibbonPageGroup.AllowTextClipping = false;
            this._naivgationRibbonPageGroup.ItemLinks.Add(this._backBarButtonItem);
            this._naivgationRibbonPageGroup.Name = "_naivgationRibbonPageGroup";
            resources.ApplyResources(this._naivgationRibbonPageGroup, "_naivgationRibbonPageGroup");
            // 
            // _moduleFunctionsRibbonPageGroup
            // 
            this._moduleFunctionsRibbonPageGroup.AllowTextClipping = false;
            this._moduleFunctionsRibbonPageGroup.ItemLinks.Add(this._startBarButtonItem);
            this._moduleFunctionsRibbonPageGroup.Name = "_moduleFunctionsRibbonPageGroup";
            resources.ApplyResources(this._moduleFunctionsRibbonPageGroup, "_moduleFunctionsRibbonPageGroup");
            // 
            // _recordRibbonPageGroup
            // 
            this._recordRibbonPageGroup.AllowTextClipping = false;
            this._recordRibbonPageGroup.ItemLinks.Add(this._openFileLocationBarButtonItem);
            this._recordRibbonPageGroup.ItemLinks.Add(this._startCsvRecordBarButtonItem);
            this._recordRibbonPageGroup.ItemLinks.Add(this._stopCsvRecordBarButtonItem);
            this._recordRibbonPageGroup.Name = "_recordRibbonPageGroup";
            resources.ApplyResources(this._recordRibbonPageGroup, "_recordRibbonPageGroup");
            // 
            // _protocolRibbonPageGroup
            // 
            this._protocolRibbonPageGroup.AllowTextClipping = false;
            this._protocolRibbonPageGroup.ItemLinks.Add(this._printProtocolBarButtonItem);
            this._protocolRibbonPageGroup.Name = "_protocolRibbonPageGroup";
            resources.ApplyResources(this._protocolRibbonPageGroup, "_protocolRibbonPageGroup");
            // 
            // _progressRibbonPageGroup
            // 
            this._progressRibbonPageGroup.AllowTextClipping = false;
            this._progressRibbonPageGroup.ItemLinks.Add(this._progressBarEditItem);
            this._progressRibbonPageGroup.Name = "_progressRibbonPageGroup";
            resources.ApplyResources(this._progressRibbonPageGroup, "_progressRibbonPageGroup");
            // 
            // _startButtonSvgImageCollection
            // 
            this._startButtonSvgImageCollection.Add("cyclical", "image://svgimages/dashboards/convertto.svg");
            this._startButtonSvgImageCollection.Add("stop", "image://svgimages/arrows/stop.svg");
            // 
            // MeasurementResults
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "MeasurementResults";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeasurementControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._repositoryItemMarqueeProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._startButtonSvgImageCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem MeasurementControlItem;
        private De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList _measurementTreeList;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _naivgationRibbonPageGroup;
        internal DevExpress.XtraBars.Ribbon.RibbonPage mainViewRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _moduleFunctionsRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _recordRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _progressRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem _backBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _startBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _openFileLocationBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _startCsvRecordBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _stopCsvRecordBarButtonItem;
        private DevExpress.Utils.SvgImageCollection _startButtonSvgImageCollection;
        private DevExpress.XtraBars.BarEditItem _progressBarEditItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar _repositoryItemMarqueeProgressBar;
        private DevExpress.XtraBars.BarButtonItem _printProtocolBarButtonItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _protocolRibbonPageGroup;
    }
}
