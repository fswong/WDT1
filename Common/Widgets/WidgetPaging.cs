using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Widgets
{
    public class WidgetPaging
    {
        #region properties
        private State _state;
        private List<string> _headers;
        private List<string> _content;
        private List<int> _responses;
        private string _footer;
        private int _offset;
        private int? _selection;
        private const int _rowsPerPage = 3;
        #endregion

        #region ctor
        public WidgetPaging(List<string> headers, List<string> content, string footer, List<int> responses = null)
        {
            _state = State.open;
            _headers = headers;
            _content = content;
            _footer = footer;
            _offset = 0;
            _responses = responses;
            Action();
        }
        #endregion


        /// <summary>
        ///  the general workflow 
        /// </summary>
        public void Action() {
            try {
                do {
                    DisplayTable();
                    HandleInput();
                } while (_state == State.open);
            }
            catch (Exception) {
                //handle error or throw?
            }
            finally {
                ReturnSelection();
            }
        }

        /// <summary>
        /// display a table with paging
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="content"></param>
        /// <param name="footer"></param>
        private void DisplayTable()
        {
            /*
             *  Header
             *  
             *  Content
             *  1.
             *  2.
             *  3.
             *  
             *  [Legend: 'N' Next Page | 'R' Return to Main Menu]
             *  
             *  Enter an input
             */

            foreach (var row in _headers) {
                Console.WriteLine(row);
            }

            Console.WriteLine(" ");

            int rowCount = 0;
            foreach (var row in _content)
            {
                if (rowCount >= _offset && rowCount < _offset + _rowsPerPage) {
                    Console.WriteLine(row);
                }
                
                rowCount++;
            }

            Console.WriteLine(" ");
            Console.WriteLine("[Legend: 'N' Next Page | 'R' Return to Main Menu]");
            Console.WriteLine(" ");

            Console.Write(_footer);
        }

        /// <summary>
        /// handles the user input, return the 
        /// </summary>
        private void HandleInput() {
            try {
                var input = Console.ReadLine();
                int parsedInput;

                if (Int32.TryParse(input, out parsedInput))
                {
                    if (_responses != null) {
                        // interger input, check if valid
                        if (_responses.IndexOf(parsedInput) != -1)
                        {
                            _selection = parsedInput;
                            _state = State.closed;
                        }
                        else {
                            Console.WriteLine("Invalid Input");
                        }
                    }
                }
                else if (input.ToUpper() == "R") {
                    // string input
                    _state = State.closed;
                }
                else if (input.ToUpper() == "N")
                {
                    // string input
                    _offset += 3;
                    if (_offset >= _content.Count) {
                        //Console.WriteLine("Reached the end of the selection");
                        //_state = State.closed;
                        _offset = 0;
                    }
                }
                else {
                    Console.WriteLine("Invalid Input");
                }
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// nullable return
        /// </summary>
        /// <returns></returns>
        private int? ReturnSelection() {
            return _selection;
        }
    }
}
