// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
namespace SO.SL
{

    /// <summary>
    /// Enum for clasificated user interactions
    /// </summary>
    public enum UserActionsEnum
    {
        Access,
        Delete,
        Insert,
        Update
    }



    /// <summary>
    /// Internal Roles, the user created roles are handled in db
    /// </summary>
    public enum UserRolEnum
    {
        /// <summary>
        /// low security checkin
        /// </summary>
        Administrator = 2
        ,
        /// <summary>
        /// No valid enum for search for string value
        /// </summary>
        NoValidEnum = -1
        ,
        /// <summary>
        /// max security checkin
        /// </summary>
        Operator = 4,
        /// <summary>
        /// no security checkin
        /// </summary>
        SuperAdministrator = 1
        ,
        /// <summary>
        /// medium security checkin
        /// </summary>
        Supervisor = 3
        ,
        /// <summary>
        /// no security checkin
        /// </summary>
        System = 0
    }




}