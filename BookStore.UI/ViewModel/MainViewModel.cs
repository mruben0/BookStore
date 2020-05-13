using BookStore.BLL.Models;
using BookStore.BLL.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BookStore.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICsvConverter _csvConverter;
        private readonly IDataService _dataService;
        private ObservableCollection<BookModel> _bookModels;
        private List<char> _separatorVariants;
        private char _separator;
        private ObservableCollection<string> _bindings;
        private bool _isCSVAttached;

        public MainViewModel(ICsvConverter csvConverter, IDataService dataService)
        {
            _csvConverter = csvConverter ?? throw new System.ArgumentNullException(nameof(csvConverter));
            _dataService = dataService ?? throw new System.ArgumentNullException(nameof(dataService));
            Separator = ';';
            SeparatorVariants = new List<char>
            {
                ';',','
            };
        }

        public ICommand AttachCSVCommand => new RelayCommand<string>((s) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                AttachCsv(openFileDialog.FileName);
        
            }
        });

        public ICommand OpenPredefinedCSVCommand => new RelayCommand(() =>
        {
            AttachCsv(@"./Resources/Books.csv");
        });       

        public ICommand ShowDescriptionCommand => new RelayCommand<string>((description) =>
        {
            MessageBox.Show(description, "Description");
        });

        public ICommand RemoveNotInStocksCommand => new RelayCommand(() =>
        {
            if (BookModels.Any(e => e.InStock == "no"))
            {
                var models = BookModels.ToList();
                models.RemoveAll(e => e.InStock == "no");
                _dataService.FixColorQuantityByPrice(models);
                BookModels = new ObservableCollection<BookModel>(models);
            }
            else
            {
                MessageBox.Show("There is nothing to remove");
            }
        });

        public ObservableCollection<string> Bindings
        {
            get { return _bindings; }
            set
            {
                _bindings = value;
                RaisePropertyChanged(nameof(Bindings));
            }
        }

        public ICommand ClearDataCommand => new RelayCommand(() =>
        {
            BookModels.Clear();
            IsCSVAttached = false;
        });

        public char Separator
        {
            get { return _separator; }
            set
            {
                _separator = value;
                RaisePropertyChanged(nameof(Separator));
            }
        }

        public bool IsCSVAttached
        {
            get { return _isCSVAttached; }
            set
            {
                _isCSVAttached = value;
                RaisePropertyChanged(nameof(IsCSVAttached));
            }
        }

        public List<char> SeparatorVariants
        {
            get { return _separatorVariants; }
            set
            {
                _separatorVariants = value;
                RaisePropertyChanged(nameof(SeparatorVariants));
            }
        }

        public ObservableCollection<BookModel> BookModels
        {
            get { return _bookModels; }
            set
            {
                _bookModels = value;
                RaisePropertyChanged(nameof(BookModels));
            }
        }

        private void AttachCsv(string path)
        {
            try
            {
                var text = File.ReadAllText(path);
                var books = _csvConverter.Convert<Book>(text, Separator).ToList();
                var bookModels = _dataService.BuildBookModelsFromBooks(books);
                Bindings = new ObservableCollection<string>(_dataService.GetBindings(books));
                BookModels = new ObservableCollection<BookModel>(bookModels);

                IsCSVAttached = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, make sure you are using right CSV or try to change separator from \"Settings\" tab");
            }
        }
    }
}