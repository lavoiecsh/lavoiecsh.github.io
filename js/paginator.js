class Paginator {
    constructor(pageSize) {
        this.pageSize = pageSize;
        this.posts = document.querySelectorAll('ul.post-list li');
        this.pageCount = Math.ceil(this.posts.length / this.pageSize);
    }

    paginate() {
        if (this.posts.length === 0) return;
        this.addPagination();
        this.selectPage(1);
    }

    addPagination() {
        const pageListElement = document.querySelector('ul.page-list');
        for (let i = 1; i <= this.pageCount; ++i) {
            let pageElement = document.createElement('li');
            let pageAnchor = document.createElement('button');
            pageAnchor.addEventListener('click', () => this.selectPage(i));
            pageAnchor.innerText = i.toString();
            pageElement.appendChild(pageAnchor);
            pageListElement.appendChild(pageElement);
        }
        this.pages = document.querySelectorAll('ul.page-list li');
    }

    selectPage(page) {
        for (let i = 1; i <= this.pages.length; ++i) {
            if (i === page)
                this.pages[i-1].classList.add('current-page');
            else
                this.pages[i-1].classList.remove('current-page');
        }
        for (let i = 1; i <= this.posts.length; ++i) {
            if (Math.ceil(i / this.pageSize) === page)
                this.posts[i-1].removeAttribute('hidden');
            else
                this.posts[i-1].setAttribute('hidden', true);
        }
    }
}
