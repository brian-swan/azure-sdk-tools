﻿// ----------------------------------------------------------------------------------
//
// Copyright 2011 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.IO;
using Microsoft.WindowsAzure.Management.CloudService.Cmdlet;
using Microsoft.WindowsAzure.Management.CloudService.Node.Cmdlet;
using Microsoft.WindowsAzure.Management.CloudService.Properties;
using Microsoft.WindowsAzure.Management.CloudService.Test.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.WindowsAzure.Management.CloudService.Test.Tests
{
    [TestClass]
    public class AddAzureNodeWorkerRoleTests : TestBase
    {
        [TestMethod]
        public void AddAzureNodeWorkerRoleProcess()
        {
            using (FileSystemHelper files = new FileSystemHelper(this))
            {
                new NewAzureServiceProjectCommand().NewAzureServiceProcess(files.RootPath, "AzureService");
                new AddAzureNodeWorkerRoleCommand().AddAzureNodeWorkerRoleProcess("WorkerRole", 1, Path.Combine(files.RootPath, "AzureService"));

                AzureAssert.ScaffoldingExists(Path.Combine(files.RootPath, "AzureService", "WorkerRole"), Path.Combine(Resources.NodeScaffolding, Resources.WorkerRole));
            }
        }
    }
}
