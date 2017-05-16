using Ht.Ihsil.Rgph.App.Superviseur.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ht.Ihsil.Rgph.App.Superviseur.MVVM
{
    public class TreeViewItemViewModel : INotifyPropertyChanged
    {
        #region VARIABLES DECLARATIONS
        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();
        private ObservableCollection<TreeViewItemViewModel> _children;
        readonly TreeViewItemViewModel _parent;
        public SqliteDataReaderService service;
        bool _isExpanded;
        bool _isSelected;
        bool _status;
        string _tip;
        string _imageSource;
        bool _isValided;
        #endregion 

        #region PROPERTIES
        ///// <summary>
        ///// Indique le type du treeview
        ///// </summary>
        //public int Type
        //{
        //    get { return type; }
        //    set { type = Value; }
        //}
        /// <summary>
        /// Le chemin de l'image indicateur sur le treeview
        /// </summary>
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    this.OnPropertyChanged("ImageSource");
                }
            }
        }

        /// <summary>
        /// LE message ToolTip sur le treeview
        /// </summary>
        public string Tip
        {
            get { return _tip; }
            set
            {
                if (_tip != value)
                {
                    _tip = value;
                    this.OnPropertyChanged("Tip");
                }
            }
        }

        /// <summary>
        /// Le statut de remplissage du treeview
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    this.OnPropertyChanged("Status");
                }
            }
        }
        
        /// <summary>
        /// Ouverture de la treeview
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }
        ////Indiquer l'objet est bien rempli
        //public bool IsFinished
        //{
        //    get { return _isFinished; }
        //    set
        //    {
        //        if (Value != _isExpanded)
        //        {
        //            _isExpanded = Value;
        //            this.OnPropertyChanged("IsFinished");
        //        }
        //    }
        //}
        ///// <summary>
        /////L'objet est mal rempli
        ///// </summary>
        //public bool IsMalRempli
        //{
        //    get { return _isMalRempli; }
        //    set
        //    {
        //        if (Value != _isMalRempli)
        //        {
        //            _isMalRempli = Value;
        //            this.OnPropertyChanged("IsMalRempli");
        //        }
        //    }
        //}
        /// <summary>
        /// L'objet n'est pas valide
        /// </summary>
        public bool IsValided
        {
            get { return _isValided; }
            set
            {
                if (value != _isValided)
                {
                    _isValided = value;
                    this.OnPropertyChanged("IsValided");
                }
            }
        }
        #endregion

        #region Constructors
        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren)
        {
            _parent = parent;
            _children = new ObservableCollection<TreeViewItemViewModel>();
            if (lazyLoadChildren)
                _children.Add(DummyChild);
            service = new SqliteDataReaderService();
        }

        // This is used to create the DummyChild instance.
        private TreeViewItemViewModel()
        {
        }

        #endregion // Constructors

        #region Children
        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get { return _children; }
            set { }
        }
        public void setChildren(ObservableCollection<TreeViewItemViewModel> _child)
        {
            this._children = _child;
        }

        #endregion // Children

        #region HasLoadedChildren

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return this.Children.Count == 1 && this.Children[0] == DummyChild; }
        }

        #endregion // HasLoadedChildren
       
        public virtual  bool IsExpand()
        {
            return this.IsExpanded;
        }

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region LoadChildren

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }
        protected virtual void Selected(){

        }

        #endregion // LoadChildren

        #region Parent

        public TreeViewItemViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members


    }
}
