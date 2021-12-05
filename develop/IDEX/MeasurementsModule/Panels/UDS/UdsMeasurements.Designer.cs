namespace MeasurementsModule.Panels.UDS
{
    internal sealed partial class UdsMeasurements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UdsMeasurements));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this._measurementTreeList = new De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this._backBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._nextBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._selectAllBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._loadConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._saveConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._removeConfigBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this._searchBarEditItem = new DevExpress.XtraBars.BarEditItem();
            this._searchEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSearchControl();
            this.mainViewRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this._navigationRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._moduleFunctionsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._configRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this._searchRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._searchEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this._measurementTreeList);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1169, 171, 530, 725);
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
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1299, 645);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Control = this._measurementTreeList;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1279, 625);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
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
            this._nextBarButtonItem,
            this._selectAllBarButtonItem,
            this._loadConfigBarButtonItem,
            this._saveConfigBarButtonItem,
            this._removeConfigBarButtonItem,
            this._searchBarEditItem});
            this.ribbonControl1.MaxItemId = 8;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.mainViewRibbonPage});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._searchEdit});
            // 
            // _backBarButtonItem
            // 
            resources.ApplyResources(this._backBarButtonItem, "_backBarButtonItem");
            this._backBarButtonItem.Id = 1;
            this._backBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_backBarButtonItem.ImageOptions.ImageIndex")));
            this._backBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_backBarButtonItem.ImageOptions.LargeImageIndex")));
            this._backBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_backBarButtonItem.ImageOptions.SvgImage")));
            this._backBarButtonItem.Name = "_backBarButtonItem";
            this._backBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnSwitchBackClicked);
            // 
            // _nextBarButtonItem
            // 
            resources.ApplyResources(this._nextBarButtonItem, "_nextBarButtonItem");
            this._nextBarButtonItem.Id = 2;
            this._nextBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_nextBarButtonItem.ImageOptions.ImageIndex")));
            this._nextBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_nextBarButtonItem.ImageOptions.LargeImageIndex")));
            this._nextBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_nextBarButtonItem.ImageOptions.SvgImage")));
            this._nextBarButtonItem.Name = "_nextBarButtonItem";
            this._nextBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnNextClick);
            // 
            // _selectAllBarButtonItem
            // 
            resources.ApplyResources(this._selectAllBarButtonItem, "_selectAllBarButtonItem");
            this._selectAllBarButtonItem.Id = 3;
            this._selectAllBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_selectAllBarButtonItem.ImageOptions.ImageIndex")));
            this._selectAllBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_selectAllBarButtonItem.ImageOptions.LargeImageIndex")));
            this._selectAllBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_selectAllBarButtonItem.ImageOptions.SvgImage")));
            this._selectAllBarButtonItem.Name = "_selectAllBarButtonItem";
            this._selectAllBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSelectAllMeasurementsClicked);
            // 
            // _loadConfigBarButtonItem
            // 
            resources.ApplyResources(this._loadConfigBarButtonItem, "_loadConfigBarButtonItem");
            this._loadConfigBarButtonItem.Id = 4;
            this._loadConfigBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_loadConfigBarButtonItem.ImageOptions.ImageIndex")));
            this._loadConfigBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_loadConfigBarButtonItem.ImageOptions.LargeImageIndex")));
            this._loadConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_loadConfigBarButtonItem.ImageOptions.SvgImage")));
            this._loadConfigBarButtonItem.Name = "_loadConfigBarButtonItem";
            this._loadConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnConfigurationLoadClick);
            // 
            // _saveConfigBarButtonItem
            // 
            resources.ApplyResources(this._saveConfigBarButtonItem, "_saveConfigBarButtonItem");
            this._saveConfigBarButtonItem.Id = 5;
            this._saveConfigBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_saveConfigBarButtonItem.ImageOptions.ImageIndex")));
            this._saveConfigBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_saveConfigBarButtonItem.ImageOptions.LargeImageIndex")));
            this._saveConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_saveConfigBarButtonItem.ImageOptions.SvgImage")));
            this._saveConfigBarButtonItem.Name = "_saveConfigBarButtonItem";
            this._saveConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnConfigurationSaveClick);
            // 
            // _removeConfigBarButtonItem
            // 
            resources.ApplyResources(this._removeConfigBarButtonItem, "_removeConfigBarButtonItem");
            this._removeConfigBarButtonItem.Id = 6;
            this._removeConfigBarButtonItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_removeConfigBarButtonItem.ImageOptions.ImageIndex")));
            this._removeConfigBarButtonItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_removeConfigBarButtonItem.ImageOptions.LargeImageIndex")));
            this._removeConfigBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_removeConfigBarButtonItem.ImageOptions.SvgImage")));
            this._removeConfigBarButtonItem.Name = "_removeConfigBarButtonItem";
            this._removeConfigBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SBtnConfigurationRemoveClick);
            // 
            // _searchBarEditItem
            // 
            resources.ApplyResources(this._searchBarEditItem, "_searchBarEditItem");
            this._searchBarEditItem.Edit = this._searchEdit;
            this._searchBarEditItem.Id = 7;
            this._searchBarEditItem.ImageOptions.ImageIndex = ((int)(resources.GetObject("_searchBarEditItem.ImageOptions.ImageIndex")));
            this._searchBarEditItem.ImageOptions.LargeImageIndex = ((int)(resources.GetObject("_searchBarEditItem.ImageOptions.LargeImageIndex")));
            this._searchBarEditItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("_searchBarEditItem.ImageOptions.SvgImage")));
            this._searchBarEditItem.Name = "_searchBarEditItem";
            // 
            // _searchEdit
            // 
            resources.ApplyResources(this._searchEdit, "_searchEdit");
            this._searchEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this._searchEdit.Name = "_searchEdit";
            // 
            // mainViewRibbonPage
            // 
            this.mainViewRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this._navigationRibbonPageGroup,
            this._moduleFunctionsRibbonPageGroup,
            this._configRibbonPageGroup,
            this._searchRibbonPageGroup});
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
            this._moduleFunctionsRibbonPageGroup.ItemLinks.Add(this._selectAllBarButtonItem);
            this._moduleFunctionsRibbonPageGroup.Name = "_moduleFunctionsRibbonPageGroup";
            resources.ApplyResources(this._moduleFunctionsRibbonPageGroup, "_moduleFunctionsRibbonPageGroup");
            // 
            // _configRibbonPageGroup
            // 
            this._configRibbonPageGroup.AllowTextClipping = false;
            this._configRibbonPageGroup.ItemLinks.Add(this._loadConfigBarButtonItem);
            this._configRibbonPageGroup.ItemLinks.Add(this._saveConfigBarButtonItem);
            this._configRibbonPageGroup.ItemLinks.Add(this._removeConfigBarButtonItem);
            this._configRibbonPageGroup.Name = "_configRibbonPageGroup";
            resources.ApplyResources(this._configRibbonPageGroup, "_configRibbonPageGroup");
            // 
            // _searchRibbonPageGroup
            // 
            this._searchRibbonPageGroup.AllowTextClipping = false;
            this._searchRibbonPageGroup.ItemLinks.Add(this._searchBarEditItem);
            this._searchRibbonPageGroup.Name = "_searchRibbonPageGroup";
            resources.ApplyResources(this._searchRibbonPageGroup, "_searchRibbonPageGroup");
            // 
            // UdsMeasurements
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "UdsMeasurements";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._searchEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private De.VwAudi.Idex.Common.Gui.Control.MeasurementTreeList _measurementTreeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _navigationRibbonPageGroup;
        internal DevExpress.XtraBars.Ribbon.RibbonPage mainViewRibbonPage;
        private DevExpress.XtraBars.BarButtonItem _backBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _nextBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _selectAllBarButtonItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _moduleFunctionsRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _configRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup _searchRibbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem _loadConfigBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _saveConfigBarButtonItem;
        private DevExpress.XtraBars.BarButtonItem _removeConfigBarButtonItem;
        private DevExpress.XtraBars.BarEditItem _searchBarEditItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchControl _searchEdit;
    }
}
