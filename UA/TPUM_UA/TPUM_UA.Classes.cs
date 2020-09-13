/* ========================================================================
 * Copyright (c) 2005-2016 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;

namespace TPUM_UA
{
    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the TransferGame ObjectType.
        /// </summary>
        public const uint TransferGame = 31;

        /// <summary>
        /// The identifier for the TransferPublisher ObjectType.
        /// </summary>
        public const uint TransferPublisher = 38;

        /// <summary>
        /// The identifier for the TransferUser ObjectType.
        /// </summary>
        public const uint TransferUser = 42;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the TransferGame_ID Variable.
        /// </summary>
        public const uint TransferGame_ID = 32;

        /// <summary>
        /// The identifier for the TransferGame_Title Variable.
        /// </summary>
        public const uint TransferGame_Title = 33;

        /// <summary>
        /// The identifier for the TransferGame_Rating Variable.
        /// </summary>
        public const uint TransferGame_Rating = 34;

        /// <summary>
        /// The identifier for the TransferGame_Publisher Variable.
        /// </summary>
        public const uint TransferGame_Publisher = 35;

        /// <summary>
        /// The identifier for the TransferGame_Premiere Variable.
        /// </summary>
        public const uint TransferGame_Premiere = 36;

        /// <summary>
        /// The identifier for the TransferGame_Genres Variable.
        /// </summary>
        public const uint TransferGame_Genres = 37;

        /// <summary>
        /// The identifier for the TransferPublisher_ID Variable.
        /// </summary>
        public const uint TransferPublisher_ID = 39;

        /// <summary>
        /// The identifier for the TransferPublisher_Name Variable.
        /// </summary>
        public const uint TransferPublisher_Name = 40;

        /// <summary>
        /// The identifier for the TransferPublisher_Country Variable.
        /// </summary>
        public const uint TransferPublisher_Country = 41;

        /// <summary>
        /// The identifier for the TransferUser_ID Variable.
        /// </summary>
        public const uint TransferUser_ID = 43;

        /// <summary>
        /// The identifier for the TransferUser_Username Variable.
        /// </summary>
        public const uint TransferUser_Username = 44;

        /// <summary>
        /// The identifier for the TransferUser_Password Variable.
        /// </summary>
        public const uint TransferUser_Password = 45;
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the TransferGame ObjectType.
        /// </summary>
        public static readonly NodeId TransferGame = new NodeId(TPUM_UA.ObjectTypes.TransferGame);

        /// <summary>
        /// The identifier for the TransferPublisher ObjectType.
        /// </summary>
        public static readonly NodeId TransferPublisher = new NodeId(TPUM_UA.ObjectTypes.TransferPublisher);

        /// <summary>
        /// The identifier for the TransferUser ObjectType.
        /// </summary>
        public static readonly NodeId TransferUser = new NodeId(TPUM_UA.ObjectTypes.TransferUser);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the TransferGame_ID Variable.
        /// </summary>
        public static readonly NodeId TransferGame_ID = new NodeId(TPUM_UA.Variables.TransferGame_ID);

        /// <summary>
        /// The identifier for the TransferGame_Title Variable.
        /// </summary>
        public static readonly NodeId TransferGame_Title = new NodeId(TPUM_UA.Variables.TransferGame_Title);

        /// <summary>
        /// The identifier for the TransferGame_Rating Variable.
        /// </summary>
        public static readonly NodeId TransferGame_Rating = new NodeId(TPUM_UA.Variables.TransferGame_Rating);

        /// <summary>
        /// The identifier for the TransferGame_Publisher Variable.
        /// </summary>
        public static readonly NodeId TransferGame_Publisher = new NodeId(TPUM_UA.Variables.TransferGame_Publisher);

        /// <summary>
        /// The identifier for the TransferGame_Premiere Variable.
        /// </summary>
        public static readonly NodeId TransferGame_Premiere = new NodeId(TPUM_UA.Variables.TransferGame_Premiere);

        /// <summary>
        /// The identifier for the TransferGame_Genres Variable.
        /// </summary>
        public static readonly NodeId TransferGame_Genres = new NodeId(TPUM_UA.Variables.TransferGame_Genres);

        /// <summary>
        /// The identifier for the TransferPublisher_ID Variable.
        /// </summary>
        public static readonly NodeId TransferPublisher_ID = new NodeId(TPUM_UA.Variables.TransferPublisher_ID);

        /// <summary>
        /// The identifier for the TransferPublisher_Name Variable.
        /// </summary>
        public static readonly NodeId TransferPublisher_Name = new NodeId(TPUM_UA.Variables.TransferPublisher_Name);

        /// <summary>
        /// The identifier for the TransferPublisher_Country Variable.
        /// </summary>
        public static readonly NodeId TransferPublisher_Country = new NodeId(TPUM_UA.Variables.TransferPublisher_Country);

        /// <summary>
        /// The identifier for the TransferUser_ID Variable.
        /// </summary>
        public static readonly NodeId TransferUser_ID = new NodeId(TPUM_UA.Variables.TransferUser_ID);

        /// <summary>
        /// The identifier for the TransferUser_Username Variable.
        /// </summary>
        public static readonly NodeId TransferUser_Username = new NodeId(TPUM_UA.Variables.TransferUser_Username);

        /// <summary>
        /// The identifier for the TransferUser_Password Variable.
        /// </summary>
        public static readonly NodeId TransferUser_Password = new NodeId(TPUM_UA.Variables.TransferUser_Password);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the Country component.
        /// </summary>
        public const string Country = "Country";

        /// <summary>
        /// The BrowseName for the Genres component.
        /// </summary>
        public const string Genres = "Genres";

        /// <summary>
        /// The BrowseName for the ID component.
        /// </summary>
        public const string ID = "ID";

        /// <summary>
        /// The BrowseName for the Name component.
        /// </summary>
        public const string Name = "Name";

        /// <summary>
        /// The BrowseName for the Password component.
        /// </summary>
        public const string Password = "Password";

        /// <summary>
        /// The BrowseName for the Premiere component.
        /// </summary>
        public const string Premiere = "Premiere";

        /// <summary>
        /// The BrowseName for the Publisher component.
        /// </summary>
        public const string Publisher = "Publisher";

        /// <summary>
        /// The BrowseName for the Rating component.
        /// </summary>
        public const string Rating = "Rating";

        /// <summary>
        /// The BrowseName for the Title component.
        /// </summary>
        public const string Title = "Title";

        /// <summary>
        /// The BrowseName for the TransferGame component.
        /// </summary>
        public const string TransferGame = "TransferGame";

        /// <summary>
        /// The BrowseName for the TransferPublisher component.
        /// </summary>
        public const string TransferPublisher = "TransferPublisher";

        /// <summary>
        /// The BrowseName for the TransferUser component.
        /// </summary>
        public const string TransferUser = "TransferUser";

        /// <summary>
        /// The BrowseName for the Username component.
        /// </summary>
        public const string Username = "Username";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the ua namespace (.NET code namespace is 'TPUM_UA').
        /// </summary>
        public const string ua = "http://opcfoundation.org/UA/";
    }
    #endregion

}