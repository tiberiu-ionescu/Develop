// Copyright (c) AUDI AG 2020
// 
// Autor: Josovic, Nenad (I/EE-87, extern)
// Erstellt: 23.02.2021

namespace SpecialTools.ProductionModeService
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using Contracts.Exceptions;
    using Contracts.Interfaces;
    using JetBrains.Annotations;
    using Messaging;
    using Properties;
    using Views;

    /// <summary>
    /// Startklasse für das ProductionModeService Special Tool
    /// </summary>
    [PublicAPI]
    [Export(typeof(ISpecialTool))]
    public sealed class ProductionModeService : ISpecialTool
    {
        /// <summary>
        /// Dateiname von dem Svg Icon
        /// </summary>
        private const string SVG_ICON_NAME = "SpecialTools.ProductionModeService.Resources.productionMode.svg";

        /// <summary>
        /// Dateiname von dem Bitmap Icon
        /// </summary>
        private const string BITPAM_ICON_NAME = "SpecialTools.ProductionModeService.Resources.productionMode.png";

        /// <summary>
        /// Referenz auf das Hauptfenster des Tools
        /// </summary>
        private MainView _mainView;

        /// <summary>
        /// Referenz auf den Communicator für das Messaging
        /// </summary>
        private ProductionModeServiceMessageAbstractor _messageAbstractor;

        /// <summary>
        /// Konstruktor initialisiert das Tool
        /// </summary>
        public ProductionModeService()
        {
            _messageAbstractor = ProductionModeServiceMessageAbstractor.GetDefaultCommunicator();

            Id = Guid.NewGuid().ToString();

            AddToolInformation();
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public UserControl GetMainView()
        {
            if (_mainView == null)
            {
                _mainView = new MainView();
                _mainView.Initialize();
            }

            return _mainView;
        }

        /// <inheritdoc />
        public UserControl GetOptionView()
        {
            throw new NoOptionsViewException();
        }

        /// <inheritdoc />
        public void Shutdown()
        {
            _messageAbstractor.Shutdown();
            _mainView?.Cancellation?.Cancel();
        }

        /// <summary>
        /// Füllt die Tool Informationen aus
        /// </summary>
        private void AddToolInformation()
        {
            ToolInformation.Id = Id;
            ToolInformation.ToolName = Resources.TOOL_NAME;
            ToolInformation.LocalizedToolName = Resources.LOCALIZED_TOOL_NAME;
            ToolInformation.LocalizedGroupName = Resources.LOCALIZED_GROUP_NAME;
            ToolInformation.LocalizedToolDescription = Resources.LOCALIZED_TOOL_DESCRIPTION;
            ToolInformation.ImageSvg = ReadSvgImageAsByteArray();
            ToolInformation.ImageBitmap = ReadBitmapImageAsByteArray();
        }

        /// <summary>
        /// Liest das Bitmap Icon aus den Resourcen und gibt diesen als Byte Array zurück
        /// </summary>
        /// <returns>Icon als Byte Array</returns>
        private byte[] ReadBitmapImageAsByteArray()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(BITPAM_ICON_NAME))
            {
                using (var memStream = new MemoryStream())
                {
                    stream?.CopyTo(memStream);
                    return memStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Liest das Svg Icon aus den Resourcen und gibt diesen als Byte Array zurück
        /// </summary>
        /// <returns>Icon als Byte Array</returns>
        private byte[] ReadSvgImageAsByteArray()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SVG_ICON_NAME))
            {
                using (var memStream = new MemoryStream())
                {
                    stream?.CopyTo(memStream);
                    return memStream.ToArray();
                }
            }
        }
    }
}
