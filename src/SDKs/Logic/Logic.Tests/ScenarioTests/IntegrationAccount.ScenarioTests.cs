﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

namespace Test.Azure.Management.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Azure.Management.Logic;
    using Microsoft.Azure.Management.Logic.Models;
    using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
    using Newtonsoft.Json.Linq;
    using Xunit;

    /// <summary>
    /// Scenario tests for the integration accounts.
    /// </summary>
    [Collection("IntegrationAccountScenarioTests")]
    public class IntegrationAccountScenarioTests : ScenarioTestsBase
    {
        /// <summary>
        /// Tests the create and delete operations of the integration account.
        /// </summary>
        [Fact(Skip = "After upgrade to vs2017, starts failing. Needs investigation")]
        public void CreateAndDeleteIntegrationAccount()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();
                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));

                // Get the IntegrationAccount and verify the content
                Assert.Equal(createdAccount.Name, integrationAccountName);

                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the create, update and delete operations of the integration account.
        /// </summary>
        [Fact(Skip = "Incorrect number of arguments supplied")]
        public void CreateAndUpdateIntegrationAccount()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));

                // Get the IntegrationAccount and verify the content
                Assert.NotNull(createdAccount);
                Assert.Equal(createdAccount.Name, integrationAccountName);

                var updatedAccount = client.IntegrationAccounts.CreateOrUpdate(
                    resourceGroupName: Constants.DefaultResourceGroup,
                    integrationAccountName: integrationAccountName,
                    integrationAccount: new IntegrationAccount
                    {
                        Location = Constants.DefaultLocation,
                        Sku = new IntegrationAccountSku()
                        {
                            Name = IntegrationAccountSkuName.Standard
                        },
                        Properties = new JObject()
                    });

                Assert.NotNull(updatedAccount);
                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the create and get(by account name) operations of the integration account.
        /// </summary>
        [Fact(Skip = "After upgrade to vs2017, starts failing. Needs investigation")]
        public void CreateAndGetIntegrationAccountByName()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));
                Assert.NotNull(createdAccount);

                // Get the IntegrationAccount and verify the content
                var getAccount = client.IntegrationAccounts.Get(Constants.DefaultResourceGroup, createdAccount.Name);
                Assert.Equal(createdAccount.Name, getAccount.Name);

                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the create and list (by subscription name) operations of the integration account.
        /// </summary>
        [Fact(Skip = "Incorrect number of arguments supplied")]
        public void ListIntegrationAccountBySubscription()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));
                Assert.NotNull(createdAccount);

                // Get the IntegrationAccount and verify the content
                var accounts = client.IntegrationAccounts.ListBySubscription();
                Assert.True(accounts.Any());

                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the create and list (by resource group name) operations of the integration account.
        /// </summary>
        [Fact(Skip = "After upgrade to vs2017, starts failing. Needs investigation")]
        public void ListIntegrationAccountByResourceGroup()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));

                // Get the IntegrationAccount and verify the content
                var accounts = client.IntegrationAccounts.ListByResourceGroup(Constants.DefaultResourceGroup);

                Assert.True(accounts.Any());

                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the create and update (by account name) operations of the integration account.
        /// </summary>
        [Fact(Skip = "After upgrade to vs2017, starts failing. Needs investigation")]
        public void UpdateIntegrationAccount()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));

                // Get the IntegrationAccount and verify the content
                Assert.NotNull(createdAccount);
                Assert.Equal(createdAccount.Name, integrationAccountName);

                IDictionary<string, string> tags = new Dictionary<string, string>();
                tags.Add("IntegrationAccount", integrationAccountName);

                //Only the tags property can be patched
                var updatedAccount = client.IntegrationAccounts.Update(
                    resourceGroupName: Constants.DefaultResourceGroup,
                    integrationAccountName: integrationAccountName,
                    integrationAccount: new IntegrationAccount
                    {
                        Tags = tags
                    });

                Assert.NotNull(updatedAccount);
                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);

            }
        }

        /// <summary>
        /// Tests the integartion account callback URL.
        /// </summary>
        [Fact(Skip = "After upgrade to vs2017, starts failing. Needs investigation")]
        public void ListIntegrationAccountCallbackUrl()
        {
            using (MockContext context = MockContext.Start(this.testClassName))
            {
                string integrationAccountName = TestUtilities.GenerateName(Constants.IntegrationAccountPrefix);
                var client = context.GetServiceClient<LogicManagementClient>();

                // Create a IntegrationAccount
                var createdAccount = client.IntegrationAccounts.CreateOrUpdate(Constants.DefaultResourceGroup,
                    integrationAccountName,
                    CreateIntegrationAccountInstance(integrationAccountName));

                // Get the IntegrationAccount and verify the content
                var callbackUrl1 = client.IntegrationAccounts.GetCallbackUrl(Constants.DefaultResourceGroup,
                    integrationAccountName, new GetCallbackUrlParameters());

                Assert.NotNull(callbackUrl1);

                var callbackUrl2 = client.IntegrationAccounts.GetCallbackUrl(Constants.DefaultResourceGroup,
                    integrationAccountName, new GetCallbackUrlParameters());

                Assert.NotNull(callbackUrl2);

                var callbackUrl3 = client.IntegrationAccounts.GetCallbackUrl(Constants.DefaultResourceGroup,
                integrationAccountName, new GetCallbackUrlParameters(DateTime.Today.AddDays(10),KeyType.Primary));

                Assert.NotNull(callbackUrl3);

                var callbackUrl4= client.IntegrationAccounts.GetCallbackUrl(Constants.DefaultResourceGroup,
                integrationAccountName, new GetCallbackUrlParameters(keyType: KeyType.Primary));

                Assert.NotNull(callbackUrl4);

                var callbackUrl5 = client.IntegrationAccounts.GetCallbackUrl(Constants.DefaultResourceGroup,
                integrationAccountName, new GetCallbackUrlParameters(notAfter: DateTime.Today.AddDays(10)));

                Assert.NotNull(callbackUrl5);

                // Delete the IntegrationAccount
                client.IntegrationAccounts.Delete(Constants.DefaultResourceGroup, integrationAccountName);
            }
        }

    }
}