//-----------------------------------------------------------------------
// <copyright file="NancySelfHostFactory.cs">
//     Copyright (c) 2016 Adam Craven. All rights reserved.
// </copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

namespace ChannelAdam.Nancy.Hosting.Self
{
    using System;
    using System.Threading.Tasks;

    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;

    public static class NancySelfHostFactory
    {
        public static HostConfiguration CreateHostConfiguration()
        {
            return new HostConfiguration
            {
                RewriteLocalhost = false    // ensure elevated permissions are NOT required to start Nancy
            };
        }

        public static NancyHost CreateAndStartNancyHost(params Uri[] baseUris)
        {
            return CreateAndStartNancyHost(CreateHostConfiguration(), baseUris);
        }

        public static NancyHost CreateAndStartNancyHost(HostConfiguration hostConfig, params Uri[] baseUris)
        {
            return CreateAndStartNancyHost(new DefaultNancyBootstrapper(), hostConfig, baseUris);
        }

        public static NancyHost CreateAndStartNancyHost(INancyBootstrapper bootstrapper, params Uri[] baseUris)
        {
            return CreateAndStartNancyHost(bootstrapper, CreateHostConfiguration(), baseUris);
        }

        public static NancyHost CreateAndStartNancyHost(INancyBootstrapper bootstrapper, HostConfiguration hostConfig, params Uri[] baseUris)
        {
            var host = new NancyHost(bootstrapper, hostConfig, baseUris);
            host.Start();
            return host;
        }

        public static NancyHost CreateAndStartNancyHostInBackgroundThread(params Uri[] baseUris)
        {
            return CreateAndStartNancyHostInBackgroundThread(CreateHostConfiguration(), baseUris);
        }

        public static NancyHost CreateAndStartNancyHostInBackgroundThread(HostConfiguration hostConfig, params Uri[] baseUris)
        {
            return StartAndWaitForChildTaskToComplete(() => CreateAndStartNancyHost(hostConfig, baseUris));
        }

        public static NancyHost CreateAndStartNancyHostInBackgroundThread(INancyBootstrapper bootstrapper, params Uri[] baseUris)
        {
            return StartAndWaitForChildTaskToComplete(() => CreateAndStartNancyHost(bootstrapper, CreateHostConfiguration(), baseUris));
        }

        public static NancyHost CreateAndStartNancyHostInBackgroundThread(INancyBootstrapper bootstrapper, HostConfiguration hostConfig, params Uri[] baseUris)
        {
            return StartAndWaitForChildTaskToComplete(() => CreateAndStartNancyHost(bootstrapper, hostConfig, baseUris));
        }

        #region Private Methods

        private static NancyHost StartAndWaitForChildTaskToComplete(Func<NancyHost> action)
        {
            return Task.Factory.StartNew(action, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent).Result;
        }

        #endregion
    }
}
