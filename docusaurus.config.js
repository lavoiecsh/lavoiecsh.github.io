// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

const copyright = `Copyright © ${new Date().getFullYear()} Sebastien Lavoie-Courchesne.`;

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
    onBrokenMarkdownLinks: 'throw',

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
                docs: {
                    routeBasePath: '/docs',
                },
                blog: {
                    showReadingTime: true,
                    routeBasePath: '/',
                    blogTitle: 'Proper Code',
                    blogDescription: 'A blog about proper coding',
                    postsPerPage: 5,
                    feedOptions: {
                        type: 'all',
                        copyright,
                        title: 'Proper Code',
                        description: 'A blog about proper coding',
                        language: 'en',
                        createFeedItems: ({ blogPosts, defaultCreateFeedItems, ...rest }) =>
                            defaultCreateFeedItems({
                                blogPosts: blogPosts.slice(0, 5),
                                ...rest,
                            }),
                    },
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
                                label: 'About me',
                                to: 'docs/about-me',
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
                    {
                        title: 'Feeds',
                        items: [
                            { label: 'RSS', href: '/rss.xml' },
                            { label: 'Atom', href: '/atom.xml' },
                            { label: 'JSON', href: '/feed.json' },
                        ],
                    },
                ],
                copyright,
            },
            prism: {
                theme: lightCodeTheme,
                darkTheme: darkCodeTheme,
            },
        }),
};

module.exports = config;
