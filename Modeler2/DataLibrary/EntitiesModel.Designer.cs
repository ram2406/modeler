﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("EntitiesDataBaseModel", "C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(DataLibrary.KindOfPrimitive), "KindOfParameter", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DataLibrary.KindOfParameter), true)]
[assembly: EdmRelationshipAttribute("EntitiesDataBaseModel", "C_Parameter_KindOfParameter", "KindOfParameter", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(DataLibrary.KindOfParameter), "Parameter", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DataLibrary.Parameter), true)]
[assembly: EdmRelationshipAttribute("EntitiesDataBaseModel", "C_Primitive_KindOfPrimitive", "KindOfPrimitive", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(DataLibrary.KindOfPrimitive), "Primitive", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DataLibrary.Primitive), true)]
[assembly: EdmRelationshipAttribute("EntitiesDataBaseModel", "C_Parameter_Primitive", "Primitive", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(DataLibrary.Primitive), "Parameter", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DataLibrary.Parameter), true)]

#endregion

namespace DataLibrary
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EntitiesDataBaseEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new EntitiesDataBaseEntities object using the connection string found in the 'EntitiesDataBaseEntities' section of the application configuration file.
        /// </summary>
        public EntitiesDataBaseEntities() : base("name=EntitiesDataBaseEntities", "EntitiesDataBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EntitiesDataBaseEntities object.
        /// </summary>
        public EntitiesDataBaseEntities(string connectionString) : base(connectionString, "EntitiesDataBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EntitiesDataBaseEntities object.
        /// </summary>
        public EntitiesDataBaseEntities(EntityConnection connection) : base(connection, "EntitiesDataBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<KindOfParameter> KindOfParameter
        {
            get
            {
                if ((_KindOfParameter == null))
                {
                    _KindOfParameter = base.CreateObjectSet<KindOfParameter>("KindOfParameter");
                }
                return _KindOfParameter;
            }
        }
        private ObjectSet<KindOfParameter> _KindOfParameter;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<KindOfPrimitive> KindOfPrimitive
        {
            get
            {
                if ((_KindOfPrimitive == null))
                {
                    _KindOfPrimitive = base.CreateObjectSet<KindOfPrimitive>("KindOfPrimitive");
                }
                return _KindOfPrimitive;
            }
        }
        private ObjectSet<KindOfPrimitive> _KindOfPrimitive;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Link> Link
        {
            get
            {
                if ((_Link == null))
                {
                    _Link = base.CreateObjectSet<Link>("Link");
                }
                return _Link;
            }
        }
        private ObjectSet<Link> _Link;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Parameter> Parameter
        {
            get
            {
                if ((_Parameter == null))
                {
                    _Parameter = base.CreateObjectSet<Parameter>("Parameter");
                }
                return _Parameter;
            }
        }
        private ObjectSet<Parameter> _Parameter;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Primitive> Primitive
        {
            get
            {
                if ((_Primitive == null))
                {
                    _Primitive = base.CreateObjectSet<Primitive>("Primitive");
                }
                return _Primitive;
            }
        }
        private ObjectSet<Primitive> _Primitive;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the KindOfParameter EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToKindOfParameter(KindOfParameter kindOfParameter)
        {
            base.AddObject("KindOfParameter", kindOfParameter);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the KindOfPrimitive EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToKindOfPrimitive(KindOfPrimitive kindOfPrimitive)
        {
            base.AddObject("KindOfPrimitive", kindOfPrimitive);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Link EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLink(Link link)
        {
            base.AddObject("Link", link);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Parameter EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToParameter(Parameter parameter)
        {
            base.AddObject("Parameter", parameter);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Primitive EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPrimitive(Primitive primitive)
        {
            base.AddObject("Primitive", primitive);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EntitiesDataBaseModel", Name="KindOfParameter")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class KindOfParameter : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new KindOfParameter object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static KindOfParameter CreateKindOfParameter(global::System.Int32 id, global::System.String name)
        {
            KindOfParameter kindOfParameter = new KindOfParameter();
            kindOfParameter.Id = id;
            kindOfParameter.Name = name;
            return kindOfParameter;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> KindOfPrimitiveId
        {
            get
            {
                return _KindOfPrimitiveId;
            }
            set
            {
                OnKindOfPrimitiveIdChanging(value);
                ReportPropertyChanging("KindOfPrimitiveId");
                _KindOfPrimitiveId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("KindOfPrimitiveId");
                OnKindOfPrimitiveIdChanged();
            }
        }
        private Nullable<global::System.Int32> _KindOfPrimitiveId;
        partial void OnKindOfPrimitiveIdChanging(Nullable<global::System.Int32> value);
        partial void OnKindOfPrimitiveIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive")]
        public KindOfPrimitive KindOfPrimitive
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<KindOfPrimitive> KindOfPrimitiveReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfPrimitive", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Parameter_KindOfParameter", "Parameter")]
        public EntityCollection<Parameter> Parameter
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Parameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "Parameter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Parameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "Parameter", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EntitiesDataBaseModel", Name="KindOfPrimitive")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class KindOfPrimitive : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new KindOfPrimitive object.
        /// </summary>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="id">Initial value of the Id property.</param>
        public static KindOfPrimitive CreateKindOfPrimitive(global::System.String name, global::System.Int32 id)
        {
            KindOfPrimitive kindOfPrimitive = new KindOfPrimitive();
            kindOfPrimitive.Name = name;
            kindOfPrimitive.Id = id;
            return kindOfPrimitive;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_KindOfParameter_KindOfPrimitive", "KindOfParameter")]
        public EntityCollection<KindOfParameter> KindOfParameter
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<KindOfParameter>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfParameter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<KindOfParameter>("EntitiesDataBaseModel.C_KindOfParameter_KindOfPrimitive", "KindOfParameter", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Primitive_KindOfPrimitive", "Primitive")]
        public EntityCollection<Primitive> Primitive
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Primitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "Primitive");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Primitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "Primitive", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EntitiesDataBaseModel", Name="Link")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Link : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Link object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Link CreateLink(global::System.Int32 id)
        {
            Link link = new Link();
            link.Id = id;
            return link;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PrimitiveId_start
        {
            get
            {
                return _PrimitiveId_start;
            }
            set
            {
                OnPrimitiveId_startChanging(value);
                ReportPropertyChanging("PrimitiveId_start");
                _PrimitiveId_start = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PrimitiveId_start");
                OnPrimitiveId_startChanged();
            }
        }
        private Nullable<global::System.Int32> _PrimitiveId_start;
        partial void OnPrimitiveId_startChanging(Nullable<global::System.Int32> value);
        partial void OnPrimitiveId_startChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PrimitiveId_end
        {
            get
            {
                return _PrimitiveId_end;
            }
            set
            {
                OnPrimitiveId_endChanging(value);
                ReportPropertyChanging("PrimitiveId_end");
                _PrimitiveId_end = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PrimitiveId_end");
                OnPrimitiveId_endChanged();
            }
        }
        private Nullable<global::System.Int32> _PrimitiveId_end;
        partial void OnPrimitiveId_endChanging(Nullable<global::System.Int32> value);
        partial void OnPrimitiveId_endChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EntitiesDataBaseModel", Name="Parameter")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Parameter : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Parameter object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Parameter CreateParameter(global::System.Int32 id)
        {
            Parameter parameter = new Parameter();
            parameter.Id = id;
            return parameter;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> KindOfParameterId
        {
            get
            {
                return _KindOfParameterId;
            }
            set
            {
                OnKindOfParameterIdChanging(value);
                ReportPropertyChanging("KindOfParameterId");
                _KindOfParameterId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("KindOfParameterId");
                OnKindOfParameterIdChanged();
            }
        }
        private Nullable<global::System.Int32> _KindOfParameterId;
        partial void OnKindOfParameterIdChanging(Nullable<global::System.Int32> value);
        partial void OnKindOfParameterIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> Value
        {
            get
            {
                return _Value;
            }
            set
            {
                OnValueChanging(value);
                ReportPropertyChanging("Value");
                _Value = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Value");
                OnValueChanged();
            }
        }
        private Nullable<global::System.Decimal> _Value;
        partial void OnValueChanging(Nullable<global::System.Decimal> value);
        partial void OnValueChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PrimitiveId
        {
            get
            {
                return _PrimitiveId;
            }
            set
            {
                OnPrimitiveIdChanging(value);
                ReportPropertyChanging("PrimitiveId");
                _PrimitiveId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("PrimitiveId");
                OnPrimitiveIdChanged();
            }
        }
        private Nullable<global::System.Int32> _PrimitiveId;
        partial void OnPrimitiveIdChanging(Nullable<global::System.Int32> value);
        partial void OnPrimitiveIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Parameter_KindOfParameter", "KindOfParameter")]
        public KindOfParameter KindOfParameter
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfParameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "KindOfParameter").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfParameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "KindOfParameter").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<KindOfParameter> KindOfParameterReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfParameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "KindOfParameter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<KindOfParameter>("EntitiesDataBaseModel.C_Parameter_KindOfParameter", "KindOfParameter", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Parameter_Primitive", "Primitive")]
        public Primitive Primitive
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Primitive>("EntitiesDataBaseModel.C_Parameter_Primitive", "Primitive").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Primitive>("EntitiesDataBaseModel.C_Parameter_Primitive", "Primitive").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Primitive> PrimitiveReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Primitive>("EntitiesDataBaseModel.C_Parameter_Primitive", "Primitive");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Primitive>("EntitiesDataBaseModel.C_Parameter_Primitive", "Primitive", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EntitiesDataBaseModel", Name="Primitive")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Primitive : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Primitive object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Primitive CreatePrimitive(global::System.Int32 id)
        {
            Primitive primitive = new Primitive();
            primitive.Id = id;
            return primitive;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> KindOfParameterId
        {
            get
            {
                return _KindOfParameterId;
            }
            set
            {
                OnKindOfParameterIdChanging(value);
                ReportPropertyChanging("KindOfParameterId");
                _KindOfParameterId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("KindOfParameterId");
                OnKindOfParameterIdChanged();
            }
        }
        private Nullable<global::System.Int32> _KindOfParameterId;
        partial void OnKindOfParameterIdChanging(Nullable<global::System.Int32> value);
        partial void OnKindOfParameterIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Primitive_KindOfPrimitive", "KindOfPrimitive")]
        public KindOfPrimitive KindOfPrimitive
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "KindOfPrimitive").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "KindOfPrimitive").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<KindOfPrimitive> KindOfPrimitiveReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "KindOfPrimitive");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<KindOfPrimitive>("EntitiesDataBaseModel.C_Primitive_KindOfPrimitive", "KindOfPrimitive", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EntitiesDataBaseModel", "C_Parameter_Primitive", "Parameter")]
        public EntityCollection<Parameter> Parameter
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Parameter>("EntitiesDataBaseModel.C_Parameter_Primitive", "Parameter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Parameter>("EntitiesDataBaseModel.C_Parameter_Primitive", "Parameter", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
