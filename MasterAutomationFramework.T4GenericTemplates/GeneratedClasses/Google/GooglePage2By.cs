﻿// ========================================================================
//          Copyright (C) Innovative Solutions Gross Bartosz
//                      - All Rights Reserved
// ========================================================================
//
// The source code contained or described herein and all documents related
// to the source code are owned by Innovative Solutions Gross Bartosz
// Unauthorized copying of this file, via any medium is strictly prohibited
//
// ========================================================================
//          Developed by Bartosz Gross, grossbartosz@gmail.com
// ========================================================================
// ------------------------------------------------------------------------
namespace MasterAutomationFramework.T4MasterAutomationFramework.T4GenericTemplates.Pages.Google
{
	using System.Diagnostics.CodeAnalysis;
	using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
	using System.Collections.Generic;
	using MasterAutomationFramework.SeleniumAPI.Extension;
	using MasterAutomationFramework.T4GenericTemplates.GeneratedClasses;

	// This is auto-generated code
	[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public abstract class GooglePage2By : WebPageBase
	{
		protected abstract IWebDriver WebDriver { get; }
		
        private Dictionary<string, By> elementsSelectors;

        public override Dictionary<string, By> Selectors
        {
            get
            {
                if (elementsSelectors == null)
                {
                    elementsSelectors = GetByReflector.GetAllElementLocators(typeof(GooglePage2));
                }

                return elementsSelectors;
            }
        }
	
		[FindsBy(How = How.XPath, Using = "//*[@id='tsf']/div[2]/div[1]/div[1]/div/div[2]/input")]
		public IWebElement SearchInputElement => this.WebDriver.FindElement(this.SearchInputBy);
		protected By SearchInputBy => By.XPath("//*[@id='tsf']/div[2]/div[1]/div[1]/div/div[2]/input");
	
		[FindsBy(How = How.XPath, Using = "//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img")]
		public IWebElement NoobImageElement => this.WebDriver.FindElement(this.NoobImageBy);
		protected By NoobImageBy => By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
	
		[FindsBy(How = How.XPath, Using = "//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img")]
		public IWebElement NoobImage2Element => this.WebDriver.FindElement(this.NoobImage2By);
		protected By NoobImage2By => By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
	
		[FindsBy(How = How.XPath, Using = "//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img")]
		public IWebElement NoobImage3Element => this.WebDriver.FindElement(this.NoobImage3By);
		protected By NoobImage3By => By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
	
		[FindsBy(How = How.XPath, Using = "//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img")]
		public IWebElement NoobImage4Element => this.WebDriver.FindElement(this.NoobImage4By);
		protected By NoobImage4By => By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
	
		[FindsBy(How = How.XPath, Using = "//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img")]
		public IWebElement NoobImage5Element => this.WebDriver.FindElement(this.NoobImage5By);
		protected By NoobImage5By => By.XPath("//*[@id='main']/ul[1]/li[3]/a/ul/li[1]/img");
	}
}

