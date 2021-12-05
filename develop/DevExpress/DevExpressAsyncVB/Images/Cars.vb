Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata

Namespace DevExpress.Xpo.Demos
    Public Class Cars
        Inherits XPLiteObject
        Private fID As Integer
        <Key()> _
        Public Property ID() As Integer
            Get
                Return fID
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)(NameOf(ID), fID, value)
            End Set
        End Property
        Private fTrademark As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Trademark() As String
            Get
                Return fTrademark
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Trademark), fTrademark, value)
            End Set
        End Property
        Private fModel As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Model() As String
            Get
                Return fModel
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Model), fModel, value)
            End Set
        End Property
        Private fHP As Short
        Public Property HP() As Short
            Get
                Return fHP
            End Get
            Set(ByVal value As Short)
                SetPropertyValue(Of Short)(NameOf(HP), fHP, value)
            End Set
        End Property
        Private fLiter As Double
        Public Property Liter() As Double
            Get
                Return fLiter
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)(NameOf(Liter), fLiter, value)
            End Set
        End Property
        Private fCyl As Byte
        Public Property Cyl() As Byte
            Get
                Return fCyl
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)(NameOf(Cyl), fCyl, value)
            End Set
        End Property
        Private fCategory As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Category() As String
            Get
                Return fCategory
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Category), fCategory, value)
            End Set
        End Property
        Private fDescription As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Description() As String
            Get
                Return fDescription
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Description), fDescription, value)
            End Set
        End Property
        Private fHyperlink As String
        <Size(SizeAttribute.Unlimited)> _
        Public Property Hyperlink() As String
            Get
                Return fHyperlink
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)(NameOf(Hyperlink), fHyperlink, value)
            End Set
        End Property
        Private fIcon() As Byte
        <Size(SizeAttribute.Unlimited)> _
        Public Property Icon() As Byte()
            Get
                Return fIcon
            End Get
            Set(ByVal value As Byte())
                SetPropertyValue(Of Byte())(NameOf(Icon), fIcon, value)
            End Set
        End Property
        Private fPicture As System.Drawing.Image
        <Size(SizeAttribute.Unlimited), ValueConverter(GetType(DevExpress.Xpo.Metadata.ImageValueConverter))> _
        Public Property Picture() As System.Drawing.Image
            Get
                Return fPicture
            End Get
            Set(ByVal value As System.Drawing.Image)
                SetPropertyValue(Of System.Drawing.Image)(NameOf(Picture), fPicture, value)
            End Set
        End Property
        Private fPrice As Decimal
        Public Property Price() As Decimal
            Get
                Return fPrice
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)(NameOf(Price), fPrice, value)
            End Set
        End Property
        Private fDeliveryDate As DateTime
        <Persistent("Delivery Date")> _
        Public Property DeliveryDate() As DateTime
            Get
                Return fDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)(NameOf(DeliveryDate), fDeliveryDate, value)
            End Set
        End Property
        Private fIsInStock As Boolean
        <Persistent("Is In Stock")> _
        Public Property IsInStock() As Boolean
            Get
                Return fIsInStock
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)(NameOf(IsInStock), fIsInStock, value)
            End Set
        End Property
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub
    End Class
End Namespace
