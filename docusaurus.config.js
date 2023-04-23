// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').Config} */
const config = {
    title: 'Proper Code',
    tagline: 'Proper coding practices',
    favicon: 'img/favicon.ico',

    // Set the production url of your site here
    url: 'https://lavoiecsh.github.io/',
    // Set the /<baseUrl>/ pathname under which your site is served
    // For GitHub pages deployment, it is often '/<projectName>/'
    baseUrl: '/',

    // GitHub pages deployment config.
    // If you aren't using GitHub pages, you don't need these.
    organizationName: 'lavoiecsh', // Usually your GitHub org/user name.
    projectName: 'lavoiecsh.github.io', // Usually your repo name.

    onBrokenLinks: 'throw',
    onBrokenMarkdownLinks: 'warn',

    // Even if you don't use internalization, you can use this field to set useful
    // metadata like html lang. For example, if your site is Chinese, you may want
    // to replace "en" with "zh-Hans".
    i18n: {
        defaultLocale: 'en',
        locales: ['en'],
    },

    presets: [
        [
            'classic',
            /** @type {import('@docusaurus/preset-classic').Options} */
            ({
                docs: false,
                blog: {
                    showReadingTime: true,
                    routeBasePath: '/',
                    blogTitle: 'Proper Code',
                    blogDescription: 'A blog about proper coding',
                },
                theme: {
                    customCss: require.resolve('./src/css/custom.css'),
                },
            }),
        ],
    ],

    themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
        ({
            // Replace with your project's social card
            // image: 'img/logo.svg',
            navbar: {
                title: 'Proper code',
                logo: {
                    alt: 'Proper code',
                    src: 'img/logo.svg',
                },
                items: [
                    {
                        to: 'tags',
                        label: 'Tags',
                        position: 'left',
                    },
                    {
                        href: 'https://github.com/lavoiecsh/code',
                        label: 'Code',
                        position: 'left',
                    },
                ],
            },
            footer: {
                style: 'dark',
                links: [
                    {
                        title: 'Docs',
                        items: [
                            {
                                label: 'Resume',
                                href: 'a',
                            },
                        ],
                    },
                    {
                        title: 'Links',
                        items: [
                            {
                                label: 'LinkedIn',
                                href: 'https://www.linkedin.com/in/sebastien-lavoie-courchesne/',
                            },
                            {
                                label: 'GitHub',
                                href: 'https://github.com/lavoiecsh',
                            },
                        ],
                    },
                ],
                copyright: `Copyright © ${new Date().getFullYear()} Sebastien Lavoie-Courchesne. Built with Docusaurus.`,
            },
            prism: {
                theme: lightCodeTheme,
                darkTheme: darkCodeTheme,
            },
        }),
};

module.exports = config;
