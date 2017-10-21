[33mcommit c419ea9757a9994b1c06966a5fd6c9879ea65b8e[m[33m ([m[1;36mHEAD -> [m[1;32mmaster[m[33m)[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:47:17 2017 -0400

    test style + error fix

[33mcommit bb31c28dddd723c851b09da38f589f107201ad1b[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:42:55 2017 -0400

    Use throw expression

[33mcommit 751667dc30dfb8e1a96aaa9de989da354f19aadb[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:42:33 2017 -0400

    use .editorconfig for C# code style.

[33mcommit ff70cf7d74adbfded92b51aaedbc7abc6dfdf7d6[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:41:52 2017 -0400

    Tests: reference Xunit analyzers

[33mcommit f4ffdf3e8d7e4151196a65c41ec2462dbb8948c1[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:40:52 2017 -0400

    LASI.Core code style and documentation updates

[33mcommit 75019bb85c4f51f4c2d468cd6827ae6fea78db33[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:39:38 2017 -0400

    LASI.Content: fix exception violations

[33mcommit a86bddb36d66043827a0b3aad5e4e41dd6bc2e6f[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:38:19 2017 -0400

    migrate dependency on System.Collections.Concurrent to NuGet

[33mcommit fb5790dbb2959ec9aad71474a49464aa5d969818[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:36:34 2017 -0400

    reafctor tests

[33mcommit 04ef4681b3e347b82980532c10fed1e6eb684a9b[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Tue Aug 8 13:01:18 2017 -0400

    fix abbarated namespace -> folder relationship

[33mcommit f62a7b1e8d745b95185b171fa47a732a6257430e[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Sun Jul 23 20:51:54 2017 -0400

    Refactor LASI.Core.Analysis.PatternMatching:
    
    - remove class constraint on matched value (in practice it will be a
    class but it is needlessly verbose and did not have any significant
    consequences on ease of type tests (as is now is x) that it had
    previously previously.
    
    - rename single use match methods

[33mcommit 88a0eb4c73e1fcf18d94c38f3f26f8eaf22b31e2[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Sun Jul 23 20:47:48 2017 -0400

    seal file converters; improve docs; cache

[33mcommit a649857aabfd13abe1cb3e1997eeb2f90538704d[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Sun Jul 23 20:46:45 2017 -0400

    fix test

[33mcommit 398539baa577731aefa2af67e98df60ddf59976e[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Sun Jul 23 20:45:47 2017 -0400

    de-optionalize

[33mcommit 97f836216acb8b99f76926322d198e326b10537e[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Sun Jul 23 20:43:48 2017 -0400

    refactor logging

[33mcommit 8e3f13eb76834f01e56f8e20db8c7eda1bcec464[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Thu Jul 20 00:28:39 2017 -0400

    LASI.Content: remove erroneous dependency

[33mcommit 41b965d4683143c0dfe4bcd35e9e4b85934d8694[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Thu Jul 20 00:26:03 2017 -0400

    LASI.Core.Analysis.PatternMatching
    
    Match<T>:
      - fix inconsistencies with Match<T, TResult>
      - refactor to employ C# 7.1 generic patterns
      - sealed
      - fix some documentation typos and formatting issues
    
    Match<T, TResult>:
      - fix inconsistencies with Match<T>
      - refactor to employ C# 7.1 generic patterns
      - sealed
      - fix some documentation typos and formatting issues
    
    MatchBase<T>, Match<T>, Match<T, TResult>:
      - remove constructors taking `Option<T>`
    
    MatchExtensions:
      - remove members targeting `Option<T>`

[33mcommit 11039f503e61b4bc33f05b6e60252526be15c6d4[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Thu Jul 20 00:19:08 2017 -0400

    downgrade xunit.runner.visualstudio 2.2.0 (stable)
    
    latest beta failing to run 64 bit tests.
    xunit framework itself remains at 2.3.0-beta3-build3705

[33mcommit 91349855122d3b1552c617e1f13d06a272513f98[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Wed Jul 19 23:48:27 2017 -0400

    un-nest pattern matching classes
    
    corresponds to existing namespacing and eases navigation

[33mcommit f5e1d3edaee75007bf3ba2a1ba4f7c9cd9100794[m
Author: Aluan Haddad <aluanh@gmail.com>
Date:   Mon Jul 10 12:07:45 2017 -0400

    refactor
